using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeAPI.Models
{
    public class SearchCacheModel
    {
        public int SearchCacheID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string YoutubeID { get; set; }
    }
}
