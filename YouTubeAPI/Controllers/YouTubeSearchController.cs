using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        IDatabaseService _searchService;
        private readonly IConfiguration _configuration;
        private int PAGE_SIZE;
        private bool CHECK_RECENT;
        private int DELAY;


        public YouTubeSearchController(IDatabaseService searchService, IConfiguration configuration)
        {
            _searchService = searchService;
            _configuration = configuration;
            PAGE_SIZE = _configuration.GetSection("YouTube").GetValue<int>("SearchSize");
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
