using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        readonly IDatabaseService _searchService;
        private readonly ILogger<YouTubeSearchController> _logger;
        private readonly IConfiguration _configuration;
        private readonly int PAGE_SIZE;


        public YouTubeSearchController(IDatabaseService searchService, IConfiguration configuration, ILogger<YouTubeSearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
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
            _logger.LogInformation($"Searching by {keywords}");
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
            _logger.LogInformation($"Searching for results on pageNumber {pageNumber}");
            return _searchService.SearchByPagination(pageNumber, PAGE_SIZE);
        }
    }
}
