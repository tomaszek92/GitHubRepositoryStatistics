using AutoFixture;
using GitHubRepositoryStatistics.Models.GitHub.Response;
using GitHubRepositoryStatistics.Services;
using GitHubRepositoryStatistics.Services.Abstract;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public async Task Should_return_AvgForks_equals_to_average_ForksCount_if_repositories_are_not_empty()
        {
            // Arrange
            var repositories = _fixture.CreateMany<Respository>().ToList();

            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(repositories);

            var expected = repositories.Average(r => r.ForksCount);

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgForks.ShouldBe(expected);
        }

        [Fact]
        public async Task Should_return_AvgStargazers_equals_to_average_StargazersCount_if_repositories_are_not_empty()
        {
            // Arrange
            var repositories = _fixture.CreateMany<Respository>().ToList();

            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(repositories);

            var expected = repositories.Average(r => r.StargazersCount);

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgStargazers.ShouldBe(expected);
        }

        [Fact]
        public async Task Should_return_AvgWatchers_equals_to_average_WatchersCount_if_repositories_are_not_empty()
        {
            // Arrange
            var repositories = _fixture.CreateMany<Respository>().ToList();

            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(repositories);

            var expected = repositories.Average(r => r.WatchersCount);

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgWatchers.ShouldBe(expected);
        }

        [Fact]
        public async Task Should_return_AvgSize_equals_to_average_Size_if_repositories_are_not_empty()
        {
            // Arrange
            var repositories = _fixture.CreateMany<Respository>().ToList();

            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(repositories);

            var expected = repositories.Average(r => r.Size);

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.AvgSize.ShouldBe(expected);
        }

        public static IEnumerable<object[]> AllLettersData => 
            Enumerable
                .Range('a', 'z' - 'a' + 1)
                .Select(letter => new object[] { letter });

        [Theory]
        [MemberData(nameof(AllLettersData))]
        public async Task Should_return_count_of_letter_equals_to_count_of_letter_in_repository_names(char letter)
        {
            // Arrange
            var repositories = _fixture.CreateMany<Respository>().ToList();

            _gitHubService
                .GetRepositoriesAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(repositories);

            var expected = String.Join("", repositories.Select(r => r.Name)).Count(l => Char.ToLower(l) == letter);

            // Act
            var dto = await _getUserRepositoriesStatistics.ExecuteAsync(_fixture.Create<string>(), CancellationToken.None);

            // Assert
            dto.Letters[letter.ToString()].ShouldBe(expected);
        }
    }
}
