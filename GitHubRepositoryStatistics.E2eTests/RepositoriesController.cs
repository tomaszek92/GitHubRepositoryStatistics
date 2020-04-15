using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GitHubRepositoryStatistics.E2eTests
{
    public class RepositoriesController
    {
        private readonly HttpClient _httpClient;

        public RepositoriesController()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task Should_return_success_status_code_for_get_repositories_by_owner()
        {
            // Arrange
            var url = "https://localhost:5001/repositories/microsoft";

            // Act
            var response = await _httpClient.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
