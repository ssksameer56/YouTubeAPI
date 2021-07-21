using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
using YouTubeAPI.Services;


[DisallowConcurrentExecution]
public class YouTubeCacheJob : IJob
{
    private readonly IYouTubeSearch _youtubeSearch;
    private readonly IDatabaseService _dbService;
    private readonly int DELAY;
    private readonly IConfiguration _configuration;
    private readonly bool CHECK_RECENT;
    private readonly int SEARCH_SIZE;

    public YouTubeCacheJob(IYouTubeSearch ytService, IDatabaseService dbService, IConfiguration configuration)
    {
        _youtubeSearch = ytService;
        _dbService = dbService;
        _configuration = configuration;
        DELAY = _configuration.GetSection("YouTube").GetValue<int>("Delay");
        CHECK_RECENT = _configuration.GetSection("YouTube").GetValue<bool>("CheckRecent");
        SEARCH_SIZE = _configuration.GetSection("YouTube").GetValue<int>("SearchSize");
    }

    /// <summary>
    /// Job that caches Youtube results into the Database
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task Execute(IJobExecutionContext context)
    {
        var results = _youtubeSearch.SearchForVideo("cricket", CHECK_RECENT, DELAY, SEARCH_SIZE);
        _dbService.StoreResults(results);
        return Task.CompletedTask;
    }
}