using GithubRepositoryStatistics.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GithubRepositoryStatistics.Services.Abstract
{
    public interface IGetUserRepositoriesStatistics
    {
        Task<UserRepositoriesStatisticsDto> ExecuteAsync(string owner, CancellationToken cancellationToken);
    }
}
