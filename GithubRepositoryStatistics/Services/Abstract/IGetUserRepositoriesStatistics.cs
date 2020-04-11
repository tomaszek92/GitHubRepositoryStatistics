using GitHubRepositoryStatistics.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubRepositoryStatistics.Services.Abstract
{
    public interface IGetUserRepositoriesStatistics
    {
        Task<UserRepositoriesStatisticsDto> ExecuteAsync(string owner, CancellationToken cancellationToken);
    }
}
