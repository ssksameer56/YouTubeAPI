using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeAPI.Models
{
    public class YouTubeSearchResult
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime PublishedDateTime { get; set; }
        public string YouTubeID { get; set; }
    }
}
