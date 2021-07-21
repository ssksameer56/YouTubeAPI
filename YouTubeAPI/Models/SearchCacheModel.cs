using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YouTubeAPI.Models
{
    /// <summary>
    /// Model to be used in the database
    /// </summary>
    public class SearchCacheModel
    {
        [Key]
        public int SearchCacheModelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string YoutubeID { get; set; }
    }
}
