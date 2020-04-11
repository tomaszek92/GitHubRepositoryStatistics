using GithubRepositoryStatistics.Models.GitHub.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GithubRepositoryStatistics.Services.Abstract
{
    public interface IGitHubService
    {
        Task<List<Respository>> GetRepositoriesAsync(string username, CancellationToken cancellationToken);
    }
}
