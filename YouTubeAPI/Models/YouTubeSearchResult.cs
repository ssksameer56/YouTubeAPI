using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeAPI.Models
{
    public class YouTubeSearchResult
    {
        private string youTubeURL;
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime PublishedDateTime { get; set; }
        public string YoutubeURL
        {
            get { return YoutubeURL }
            set { YoutubeURL = "youtube.com/v/" + value; }
        }
    }
}
