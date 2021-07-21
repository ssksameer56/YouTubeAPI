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
    public class YouTubeSearch
    {
        YouTubeService _youTubeService;
        public YouTubeSearch()
        {
            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "REPLACE_ME",
                ApplicationName = this.GetType().ToString()
            });
        }

        public List<YouTubeSearchResult> SearchForVideo(string keyword, bool checkRecent, int amountInMinutes, int numberOfSearches)
        {
            var searchListRequest = _youTubeService.Search.List("snippet");
            searchListRequest.Q = keyword; // Replace with your search term.
            searchListRequest.MaxResults = numberOfSearches;
            searchListRequest.Type = "video";
            if (checkRecent)
            {
                searchListRequest.PublishedAfter = DateTime.UtcNow.AddMinutes(-amountInMinutes);
            }

            // Call the search.list method to retrieve results matching the specified query term.
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
