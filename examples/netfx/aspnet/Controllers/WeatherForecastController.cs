using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AspNetExample.Controllers
{
    public class WeatherForecastController : ApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // GET: api/WeatherForecast
        public IEnumerable<WeatherForecast> Get()
        {
            var r = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = r.Next(-20, 55),
                Summary = Summaries[r.Next(0, Summaries.Length)]
            })
            .ToArray();
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }
            public int TemperatureC { get; set; }
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
            public string Summary { get; set; }
        }
    }
}
