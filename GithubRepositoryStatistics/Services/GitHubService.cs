using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GithubRepositoryStatistics.Services.Abstract;
using GithubRepositoryStatistics.Models.GitHub.Response;

namespace GithubRepositoryStatistics.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;
        
        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Respository>> GetRepositoriesAsync(string username, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"users/{username}/repos", cancellationToken);

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<List<Respository>>(responseStream, null, cancellationToken);
            }
        }
    }
}
