using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace hacker_news_angular_v1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {

        private readonly ILogger<HackerNewsController> _logger;

        public HackerNewsController(ILogger<HackerNewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<int>> Get()
        {
            try
            {
                var client = new WebClient();

                var articeIDsResponse = await client.DownloadStringTaskAsync("https://hacker-news.firebaseio.com/v0/newstories.json");

                List<int> result = JsonConvert.DeserializeObject<List<int>>(articeIDsResponse);

                return result;
            }
            catch
            {
                _logger.LogError("Issue encountered getting a list of Hacker News stories IDs");
                return null;
            }
        }

        [HttpGet("ArticleDetail")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var client = new WebClient();

                var articeDetailResponse = await client.DownloadStringTaskAsync("https://hacker-news.firebaseio.com/v0/item/" + id + ".json");

                return Ok(articeDetailResponse);
            }
            catch
            {
                _logger.LogError("Issue encountered getting a list of Hacker News stories IDs");
                return null;
            }
        }


    }
}