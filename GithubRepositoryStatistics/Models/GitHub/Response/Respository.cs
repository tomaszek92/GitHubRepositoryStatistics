using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GithubRepositoryStatistics.Models.GitHub.Response
{
    public class Respository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int StargazersCount { get; set; }
        
        [JsonPropertyName("watchers_count")]
        public int WatchersCount { get; set; }
        
        [JsonPropertyName("size")]
        public int Size { get; set; }
        
        [JsonPropertyName("forks_count")]
        public int ForksCount { get; set; }
    }
}
