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


        /// <summary>
        /// Gets YouTube results by keywords
        /// </summary>
        /// <param name="keywords">words seperated by space</param>
        /// <returns></returns>
        [HttpGet("searchKeyword")]
        public List<YouTubeSearchResult> SearchByKeyword(string keywords)
        {
            return _searchService.SearchByKeyword(keywords);
        }

        /// <summary>
        /// Search All Results from the Database
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("searchAllResults")]
        public List<YouTubeSearchResult> SearchAllResults(int pageNumber = 0)
        {
            return _searchService.SearchByPagination(pageNumber, PAGE_SIZE);
        }
    }
}
