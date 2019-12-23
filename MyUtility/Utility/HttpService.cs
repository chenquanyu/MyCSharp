using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility.Utility
{
    public class HttpService
    {
        private readonly string url;
        private readonly HttpClient httpClient;

        public HttpService(string url)
        {
            this.url = url;
            httpClient = new HttpClient();
        }

        public async Task<T> SendAsync<T>(object request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var result = await httpClient.PostAsync(new Uri(url), new StringContent(requestJson, Encoding.UTF8));
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<T>(content);
            return response;
        }

        public T Send<T>(object request)
        {
            try
            {
                return SendAsync<T>(request).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.GetBaseException();
            }
        }
    }
}
