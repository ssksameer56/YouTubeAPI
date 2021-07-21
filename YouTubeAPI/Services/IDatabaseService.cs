using System.Collections.Generic;
using YouTubeAPI.Models;

namespace YouTubeAPI.Services
{
    public interface IDatabaseService
    {
        List<YouTubeSearchResult> SearchByKeyword(string title, string description);
        List<YouTubeSearchResult> SearchByPagination(int pageNumber, int amount);
        void StoreResults(List<YouTubeSearchResult> results);
    }
}