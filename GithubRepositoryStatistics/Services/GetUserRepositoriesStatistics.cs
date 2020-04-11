using GithubRepositoryStatistics.Models;
using GithubRepositoryStatistics.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GithubRepositoryStatistics.Services
{
    public class GetUserRepositoriesStatistics : IGetUserRepositoriesStatistics
    {
        private readonly IGitHubService _gitHubService;

        public GetUserRepositoriesStatistics(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<UserRepositoriesStatisticsDto> ExecuteAsync(string owner, CancellationToken cancellationToken)
        {
            var repositories = await _gitHubService.GetRepositoriesAsync(owner, cancellationToken);

            if (!repositories.Any())
            {
                return new UserRepositoriesStatisticsDto
                {
                    Letters = InitLettersDictionary()
                };
            }

            int forksSum = 0;
            int sizeSum = 0;
            int stargazersSum = 0;
            int watchersSum = 0;
            var letters = InitLettersDictionary();

            foreach (var repository in repositories)
            {
                forksSum += repository.ForksCount;
                sizeSum += repository.Size;
                stargazersSum += repository.StargazersCount;
                watchersSum += repository.WatchersCount;

                foreach (var letter in repository.Name.Where(Char.IsLetter))
                {
                    letters[Char.ToLower(letter).ToString()]++;
                }
            }

            return new UserRepositoriesStatisticsDto
            {
                Owner = owner,
                AvgForks = GetAverage(forksSum, repositories.Count),
                AvgSize = GetAverage(sizeSum, repositories.Count),
                AvgStargazers = GetAverage(stargazersSum, repositories.Count),
                AvgWatchers = GetAverage(watchersSum, repositories.Count),
                Letters = letters
            };
        }

        private static Dictionary<string, int> InitLettersDictionary()
        {
            var letters = new Dictionary<string, int>();

            for (var letter = 'a'; letter <= 'z'; letter++)
            {
                letters[letter.ToString()] = 0;
            }

            return letters;
        }

        private static double GetAverage(int sum, int count)
        {
            return (double)sum / count;
        }
    }
}
