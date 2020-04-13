using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GitHubRepositoryStatistics.Services.Abstract;
using GitHubRepositoryStatistics.Models.GitHub.Response;
using GitHubRepositoryStatistics.Models.Exceptions;

namespace GitHubRepositoryStatistics.Services
{
    public class GitHubService : IGitHubService
    {
        private const string BaseAddress = "https://api.github.com";
        private readonly HttpClient _httpClient;
        
        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Repository>> GetRepositoriesAsync(string username, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{BaseAddress}/users/{username}/repos", cancellationToken);
            
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                ex.Data[HttpRequestExceptionData.StatusCode] = response.StatusCode;
                throw;
            }

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<List<Repository>>(responseStream, null, cancellationToken);
            }
        }
    }
}
