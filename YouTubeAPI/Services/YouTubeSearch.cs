using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using YouTubeAPI.Models;

namespace YouTubeAPI.Services
{
 
    /// <summary>
    /// Class to Search From YouTube
    /// </summary>
    public class YouTubeSearch : IYouTubeSearch
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<YouTubeSearch> _logger;
        YouTubeService _youTubeService;
        string[] _keys;
        private int keyCount = 0;
        public YouTubeSearch(ILogger<YouTubeSearch> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _keys = _configuration["YouTube:Keys"].Split(',');
            _logger = logger;
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _keys[keyCount++],
                ApplicationName = this.GetType().ToString()
            });
        }

        /// <summary>
        /// Searches For Video Results From YouTube
        /// </summary>
        /// <param name="keyword">Keyword to Search</param>
        /// <param name="checkRecent">Bool to Find Recent videos or cached ones</param>
        /// <param name="amountInSeconds">Delay in Minutes in case need recent results</param>
        /// <param name="numberOfSearches"> Number of video results to find</param>
        /// <returns></returns>
        public List<YouTubeSearchResult> SearchForVideo(string keyword, bool checkRecent, int amountInSeconds, int numberOfSearches)
        {
            _logger.LogInformation($"Getting data for {keyword} for last {amountInSeconds}");
            var searchListRequest = _youTubeService.Search.List("snippet");
            searchListRequest.Q = keyword;
            searchListRequest.MaxResults = numberOfSearches;
            searchListRequest.Type = "video";
            if (checkRecent)
            {
                searchListRequest.PublishedAfter = DateTime.UtcNow.AddSeconds(-amountInSeconds);
            }

            try
            {
                var searchListResponse = searchListRequest.ExecuteAsync();

                List<YouTubeSearchResult> videos = new();
               
                foreach (var searchResult in searchListResponse.Result.Items)
                {
                    videos.Add(new YouTubeSearchResult
                    {
                        Title = searchResult.Snippet.Title,
                        Description = searchResult.Snippet.Description,
                        PublishedDateTime = (DateTime)searchResult.Snippet.PublishedAt,
                        YouTubeID = searchResult.Id.VideoId
                    });
                }
                _logger.LogInformation($"Obtained list of {videos.Count} from YouTube");
                return videos;
            }
            catch (AggregateException e)
            {
                _logger.LogError($"Could not retrieve content from YouTube {e.Message}");
                if (e.Message.Contains("quota"))
                    UpdateAPIKey();
                throw;
            }
            
        }

        /// <summary>
        /// Update API Key of the Service
        /// </summary>
        /// <param name="key">The new Key</param>
        public void UpdateAPIKey()
        {
            var key = _keys[keyCount];
            keyCount = (keyCount++) % _keys.Length;
            _logger.LogInformation($"Updating API Key for YouTube");
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = key,
                ApplicationName = this.GetType().ToString()
            });
        }
    }
}
