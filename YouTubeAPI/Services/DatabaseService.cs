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

        public DatabaseService(YouTubeCacheContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Stores the data to Database
        /// </summary>
        /// <param name="results">Youtube results</param>
        public void StoreResults(List<YouTubeSearchResult> results)
        {
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
        public List<YouTubeSearchResult> SearchByKeyword(string title, string description)
        {
            var words = title.Split(" ").ToList();
            words.AddRange(description.Split(" "));
            var finalResults = new List<YouTubeSearchResult>();
            foreach(var word in words)
            {
                var results = _dbContext.Results.Where(x => x.Title.Contains(word)
                || x.Description.Contains(word));
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
            if(finalResults.Any())
                return finalResults.ToList().GroupBy(x => x.YouTubeID)
                        .Select(p => p.First())
                        .ToList();
            else
                return new List<YouTubeSearchResult>();
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
                return dbResults.ToList().GroupBy(x => x.YouTubeID)
                    .Select(p => p.First())
                    .ToList();
            }
            return new List<YouTubeSearchResult>();
        }
    }
}
