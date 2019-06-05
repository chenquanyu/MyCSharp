using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyWeb2.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private RpcHelper helper = new RpcHelper("https://seed1.neo.org:10331");

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();

            var res = helper.Send<object>("");

            //int a = GetVAsync().Result;

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index + startDateIndex).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        public async Task<int> GetVAsync()
        {
            await Task.Delay(1000);
            return 1;
        }

    }


    public class RpcHelper
    {
        private readonly string url;
        private readonly HttpClient httpClient;

        public RpcHelper(string url)
        {
            this.url = url;
            httpClient = new HttpClient();
        }

        public async Task<T> SendAsync<T>(string request)
        {
            var result = await httpClient.PostAsync(new Uri(url), new StringContent(request, Encoding.UTF8));
            var response = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }

        public T Send<T>(string request)
        {
            T result = default(T);
            result = SendAsync<T>(request).Result;
            //Task.Run(async () => result = await SendAsync<T>(request)).Wait();
            return result;
        }

    }
}
