using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeAPI.Models;
using YouTubeAPI.Services;

namespace YouTubeAPI.Controllers
{
    [Route("api/YouTubeAPI")]
    [ApiController]
    public class YouTubeSearchController : ControllerBase
    {
        DatabaseService _searchService;
        private readonly int PAGE_SIZE = 50;

        public YouTubeSearchController(DatabaseService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet("searchKeyword")]
        public List<YouTubeSearchResult> SearchByKeyword(string title, string description)
        {
            return _searchService.SearchByKeyword(title, description);
        }

        [HttpGet("searchAllResults")]
        public List<YouTubeSearchResult> SearchAllResults(int pageNumber = 0)
        {
            return _searchService.SearchByPagination(pageNumber, PAGE_SIZE);
        }
    }
}
