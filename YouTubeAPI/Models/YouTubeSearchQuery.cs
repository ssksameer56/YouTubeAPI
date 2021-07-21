using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeAPI.Models
{
    
    public class YouTubeSearchQuery
    {
        [JsonProperty("type")]

        public string Type { get; set; }
        [JsonProperty("keyword")] 
        public string Keyword { get; set; }

        [JsonProperty("publishedAfter")] 
        public DateTime PublishedAfter { get; set; }
    }
}
