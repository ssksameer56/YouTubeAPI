using System.Collections.Generic;
using YouTubeAPI.Models;

namespace YouTubeAPI.Services
{
    public interface IYouTubeSearch
    {
        List<YouTubeSearchResult> SearchForVideo(string keyword, bool checkRecent, int amountInMinutes, int numberOfSearches);
        void UpdateAPIKey(string key);
    }
}