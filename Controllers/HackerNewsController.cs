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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HackerNewsController> _logger;

        public HackerNewsController(ILogger<HackerNewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<int> Get()
        {
            try
            {
                var client = new WebClient();

                //ServicePoint sp = ServicePointManager.FindServicePoint(new Uri("https://hacker-news.firebaseio.com/v0/newstories.json"));
                //sp.ConnectionLimit = int.MaxValue;

                //client.Proxy = null;
                var articeIDsResponse = client.DownloadString("https://hacker-news.firebaseio.com/v0/newstories.json");

                List<int> result = JsonConvert.DeserializeObject<List<int>>(articeIDsResponse);

                return result;
            }
            catch
            {
                return null;
            }

            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }

    }
}