using GitHubRepositoryStatistics.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Net;

namespace GitHubRepositoryStatistics.Controllers
{
    [ApiController]
    [Route("repositories")]
    public class RepositoriesController : ControllerBase
    {
        [HttpGet("{owner}")]
        public async Task<IActionResult> GetUserRepositoriesStatistics(
            string owner,
            CancellationToken cancellationToken,
            [FromServices] IGetUserRepositoriesStatistics getUserRepositoriesStatistics)
        {
            try
            {
                var dto = await getUserRepositoriesStatistics.ExecuteAsync(owner, cancellationToken);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("StatusCode") && (HttpStatusCode)ex.Data["StatusCode"] == HttpStatusCode.NotFound)
                {
                    return NotFound();
                }
                return StatusCode(500);
            }
        }
    }
}
