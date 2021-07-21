using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeAPI.DataAccess;
using YouTubeAPI.Models;

namespace YouTubeAPI.Services
{
    public class SearchCachingService
    {
        YouTubeCacheContext _dbContext;

        public SearchCachingService(YouTubeCacheContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void StoreResults(List<YouTubeSearchResult> results)
        {
            var dbResults = results.Select(x => new SearchCacheModel
            {
                Title = x.Title,
                Description = x.Description,
                YoutubeID = x.YouTubeID,
                PublishedDateTime = x.PublishedDateTime
            });
            _dbContext.AddRange(results);
            _dbContext.SaveChanges();
        }

        public List<YouTubeSearchResult> SearchByKeyword(string title, string description)
        {
            var results = _dbContext.Results.Where(x => x.Title.Contains(title)
            || x.Description.Contains(description));
            if (results.Any())
            {
                var dbResults = results.Select(x => new YouTubeSearchResult
                {
                    Title = x.Title,
                    Description = x.Description,
                    PublishedDateTime = x.PublishedDateTime,
                    YouTubeID = x.YoutubeID
                });
                return dbResults.Distinct().ToList();
            }
            else
                return new List<YouTubeSearchResult>();
        }

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
                return dbResults.Distinct().ToList();
            }
            return new List<YouTubeSearchResult>();
        }
    }
}
