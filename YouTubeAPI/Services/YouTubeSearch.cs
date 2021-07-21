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
using YouTubeAPI.Models;

namespace YouTubeAPI.Services
{
 
    /// <summary>
    /// Class to Search From YouTube
    /// </summary>
    public class YouTubeSearch : IYouTubeSearch
    {
        YouTubeService _youTubeService;
        public YouTubeSearch()
        {
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBZRDiASxyMs4oLi106dN8d1smB-eeVrMY",
                ApplicationName = this.GetType().ToString()
            });
        }

        /// <summary>
        /// Searches For Video Results From YouTube
        /// </summary>
        /// <param name="keyword">Keyword to Search</param>
        /// <param name="checkRecent">Bool to Find Recent videos or cached ones</param>
        /// <param name="amountInMinutes">Delay in Minutes in case need recent results</param>
        /// <param name="numberOfSearches"> Number of video results to find</param>
        /// <returns></returns>
        public List<YouTubeSearchResult> SearchForVideo(string keyword, bool checkRecent, int amountInMinutes, int numberOfSearches)
        {
            var searchListRequest = _youTubeService.Search.List("snippet");
            searchListRequest.Q = keyword;
            searchListRequest.MaxResults = numberOfSearches;
            searchListRequest.Type = "video";
            if (checkRecent)
            {
                searchListRequest.PublishedAfter = DateTime.UtcNow.AddMinutes(-amountInMinutes);
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

                return videos;
            }
            catch (AggregateException e)
            {                
                Console.WriteLine(e.Message);
                throw;
            }
            
        }

        /// <summary>
        /// Update API Key of the Service
        /// </summary>
        /// <param name="key">The new Key</param>
        public void UpdateAPIKey(string key)
        {
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = key,
                ApplicationName = this.GetType().ToString()
            });
        }
    }
}
