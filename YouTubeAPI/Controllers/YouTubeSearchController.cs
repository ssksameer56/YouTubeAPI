using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace YouTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeSearchController : ControllerBase
    {
        // GET: api/<YouTubeSearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<YouTubeSearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<YouTubeSearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<YouTubeSearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<YouTubeSearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
