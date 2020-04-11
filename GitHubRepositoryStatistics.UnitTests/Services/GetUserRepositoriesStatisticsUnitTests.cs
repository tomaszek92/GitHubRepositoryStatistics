using AutoFixture;
using GithubRepositoryStatistics.Models.GitHub.Response;
using GithubRepositoryStatistics.Services;
using GithubRepositoryStatistics.Services.Abstract;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GitHubRepositoryStatistics.UnitTests.Services
{
    public class GetUserRepositoriesStatisticsUnitTests
    {
        private readonly Fixture _fixture;
        private readonly IGitHubService _gitHubService;
        private readonly GetUserRepositoriesStatistics _getUserRepositoriesStatistics;

        public GetUserRepositoriesStatisticsUnitTests()
        {
            _fixture = new Fixture();
            _gitHubService = Substitute.For<IGitHubService>();
            _getUserRepositoriesStatistics = new GetUserRepositoriesStatistics(_gitHubService);
        }

        [Fact]
        public async Task Should_return_AvgForks_equals_to_zero_if_repositories_are_empty()
        {
            // Arrange
            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new List<Respository>());

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgForks.ShouldBe(0);
        }

        [Fact]
        public async Task Should_return_AvgStargazers_equals_to_zero_if_repositories_are_empty()
        {
            // Arrange
            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new List<Respository>());

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgStargazers.ShouldBe(0);
        }

        [Fact]
        public async Task Should_return_AvgWatchers_equals_to_zero_if_repositories_are_empty()
        {
            // Arrange
            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new List<Respository>());

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgWatchers.ShouldBe(0);
        }

        [Fact]
        public async Task Should_return_AvgSize_equals_to_zero_if_repositories_are_empty()
        {
            // Arrange
            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new List<Respository>());

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgSize.ShouldBe(0);
        }

        [Fact]
        public async Task Should_return_each_letter_equals_to_zero_if_repositories_are_empty()
        {
            // Arrange
            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(new List<Respository>());

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            for (var letter = 'a'; letter <= 'z'; letter++)
            {
                dto.Letters[letter.ToString()].ShouldBe(0);
            }
        }
    }
}
