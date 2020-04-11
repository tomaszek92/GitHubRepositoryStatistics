using System.Collections.Generic;

namespace GitHubRepositoryStatistics.Models
{
    public class UserRepositoriesStatisticsDto
    {
        public string Owner { get; set; }
        public Dictionary<string, int> Letters { get; set; }
        public double AvgStargazers { get; set; }
        public double AvgWatchers { get; set; }
        public double AvgForks { get; set; }
        public double AvgSize { get;set; }
    }
}
