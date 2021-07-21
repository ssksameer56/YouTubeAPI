using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeAPI.Models;

namespace YouTubeAPI.DataAccess
{
    /// <summary>
    /// Class to handle DB Information
    /// </summary>
    public class YouTubeCacheContext : DbContext
    {
        public YouTubeCacheContext(DbContextOptions<YouTubeCacheContext> options)
            : base(options)
        {
        }

        public DbSet<SearchCacheModel> Results { get; set; }
    }
}
