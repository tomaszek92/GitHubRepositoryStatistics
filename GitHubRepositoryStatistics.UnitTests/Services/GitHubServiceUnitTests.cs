using AutoFixture;
using FluentAssertions;
using GitHubRepositoryStatistics.Models.Exceptions;
using GitHubRepositoryStatistics.Models.GitHub.Response;
using GitHubRepositoryStatistics.Services;
using Shouldly;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GitHubRepositoryStatistics.UnitTests.Services
{
    public class GitHubServiceUnitTests
    {
        private readonly Fixture _fixture;

        public GitHubServiceUnitTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Should_retrun_deserialized_list_of_repositories_if_status_code_is_200()
        {
            // Arrange
            var repositories = _fixture.CreateMany<Repository>().ToList();

            var username = _fixture.Create<string>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(repositories))
            });

            var gitHubService = CreateGitHubService(fakeHttpMessageHandler);

            // Act
            var result = await gitHubService.GetRepositoriesAsync(username, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(repositories);
        }

        [Fact]
        public async Task Should_throw_exception_with_status_code_equals_404_if_status_code_is_404()
        {
            // Arrange
            var username = _fixture.Create<string>();

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            });

            var gitHubService = CreateGitHubService(fakeHttpMessageHandler);

            // Act
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await gitHubService.GetRepositoriesAsync(username, CancellationToken.None));

            // Assert
            exception.Data[HttpRequestExceptionData.StatusCode].ShouldBe(HttpStatusCode.NotFound);
        }

        private GitHubService CreateGitHubService(FakeHttpMessageHandler fakeHttpMessageHandler)
        {
            return new GitHubService(new HttpClient(fakeHttpMessageHandler));
        }

        private class FakeHttpMessageHandler : DelegatingHandler
        {
            private readonly HttpResponseMessage _fakeResponse;

            public FakeHttpMessageHandler(HttpResponseMessage responseMessage)
            {
                _fakeResponse = responseMessage;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_fakeResponse);
            }
        }
    }
}
