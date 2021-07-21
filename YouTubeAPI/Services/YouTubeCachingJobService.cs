using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;
using YouTubeAPI.Services;


[DisallowConcurrentExecution]
public class YouTubeCacheJob : IJob
{
    private readonly YouTubeSearch _youtubeSearch;
    private readonly DatabaseService _dbService;

    public YouTubeCacheJob(YouTubeSearch ytService, DatabaseService dbService)
    {
        _youtubeSearch = ytService;
        _dbService = dbService;
    }

    /// <summary>
    /// Job that caches Youtube results into the Database
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task Execute(IJobExecutionContext context)
    {
        var results = _youtubeSearch.SearchForVideo("cricket", true, 15, 50);
        _dbService.StoreResults(results);
        return Task.CompletedTask;
    }
}