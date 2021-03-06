using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;
using YouTubeAPI.Services;


[DisallowConcurrentExecution]
public class YouTubeCacheJob : IJob
{
    private readonly IYouTubeSearch _youtubeSearch;
    private readonly IDatabaseService _dbService;
    private readonly int DELAY;
    private readonly IConfiguration _configuration;
    private readonly ILogger<YouTubeCacheJob> _logger;
    private readonly bool CHECK_RECENT;
    private readonly int SEARCH_SIZE;
    private readonly string KEYWORD;

    public YouTubeCacheJob(IYouTubeSearch ytService, IDatabaseService dbService, IConfiguration configuration, ILogger<YouTubeCacheJob> logger)
    {
        _youtubeSearch = ytService;
        _dbService = dbService;
        _configuration = configuration;
        _logger = logger;
        DELAY = _configuration.GetSection("YouTube").GetValue<int>("Delay");
        CHECK_RECENT = _configuration.GetSection("YouTube").GetValue<bool>("CheckRecent");
        SEARCH_SIZE = _configuration.GetSection("YouTube").GetValue<int>("SearchSize");
        KEYWORD = _configuration.GetSection("YouTube").GetValue<string>("keywordToSearch");
    }

    /// <summary>
    /// Job that caches Youtube results into the Database
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation("Executing Job to get data from YouTube");
            var results = _youtubeSearch.SearchForVideo(KEYWORD, CHECK_RECENT, DELAY, SEARCH_SIZE);
            _logger.LogInformation($"Obtained {results.Count} from YouTube");
            _dbService.StoreResults(results);
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Error while executing async job from YouTube - {e.Message}");
            throw;
        }
    }
}