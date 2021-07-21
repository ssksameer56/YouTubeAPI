using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeAPI.Models
{
    /// <summary>
    /// Model to map results from YouTube
    /// </summary>
    public class YouTubeSearchResult
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime PublishedDateTime { get; set; }
        public string YouTubeID { get; set; }
    }
}
