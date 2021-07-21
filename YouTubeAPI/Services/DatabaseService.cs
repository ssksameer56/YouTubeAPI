using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeAPI.DataAccess;
using YouTubeAPI.Models;

namespace YouTubeAPI.Services
{
    /// <summary>
    /// Service that handles the Database interactions
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        YouTubeCacheContext _dbContext;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(YouTubeCacheContext dbContext, ILogger<DatabaseService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Stores the data to Database
        /// </summary>
        /// <param name="results">Youtube results</param>
        public void StoreResults(List<YouTubeSearchResult> results)
        {
            _logger.LogInformation($"Storing {results.Count}");
            var dbResults = results.Select(x => new SearchCacheModel
            {
                Title = x.Title,
                Description = x.Description,
                YoutubeID = x.YouTubeID,
                PublishedDateTime = x.PublishedDateTime
            });
            _dbContext.AddRange(dbResults);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Searches database for videos with specific keyword
        /// </summary>
        /// <param name="title">keyword to search in title</param>
        /// <param name="description">keyword to search in description</param>
        /// <returns></returns>
        public List<YouTubeSearchResult> SearchByKeyword(string keyword)
        {
            var words = keyword.Split(" ").ToList();
            var finalResults = new List<YouTubeSearchResult>();
            foreach(var word in words)
            {
                var results = _dbContext.Results.Where(x => x.Title.Contains(word,StringComparison.CurrentCultureIgnoreCase)
                || x.Description.Contains(word,StringComparison.CurrentCultureIgnoreCase));
                if (results.Any())
                {
                    var dbResults = results.Select(x => new YouTubeSearchResult
                    {
                        Title = x.Title,
                        Description = x.Description,
                        PublishedDateTime = x.PublishedDateTime,
                        YouTubeID = x.YoutubeID
                    });
                    finalResults.AddRange(dbResults);
                }
            }
            _logger.LogInformation($"Retrieving {finalResults.Count}");
            if (finalResults.Any())
                return finalResults
                        .ToList();
            else
            {
                _logger.LogInformation($"No results");
                return new List<YouTubeSearchResult>();
            }
        }

        /// <summary>
        /// Searches all data in page format
        /// </summary>
        /// <param name="pageNumber">the page number to return </param>
        /// <param name="amount">number of records in a page</param>
        /// <returns></returns>
        public List<YouTubeSearchResult> SearchByPagination(int pageNumber, int amount)
        {
            var results = _dbContext.Results.OrderByDescending(x => x.PublishedDateTime)
                .Skip(pageNumber * amount)
                .Take(amount);
            if (results.Any())
            {
                var dbResults = results.Select(x => new YouTubeSearchResult
                {
                    Title = x.Title,
                    Description = x.Description,
                    PublishedDateTime = x.PublishedDateTime,
                    YouTubeID = x.YoutubeID
                });
                _logger.LogInformation($"Retrieving {dbResults.Count()}");
                return dbResults
                    .ToList();
            }
            _logger.LogInformation($"No results");
            return new List<YouTubeSearchResult>();
        }
    }
}
