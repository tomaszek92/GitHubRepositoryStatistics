using GitHubRepositoryStatistics.Models;
using GitHubRepositoryStatistics.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;

namespace GitHubRepositoryStatistics.Controllers
{
    [ApiController]
    [Route("repositories")]
    public class RepositoriesController : ControllerBase
    {
        [HttpGet("{owner}")]
        public async Task<UserRepositoriesStatisticsDto> GetUserRepositoriesStatistics(
            string owner,
            CancellationToken cancellationToken,
            [FromServices] IGetUserRepositoriesStatistics getUserRepositoriesStatistics)
        {
            return await getUserRepositoriesStatistics.ExecuteAsync(owner, cancellationToken);
        }
    }
}
