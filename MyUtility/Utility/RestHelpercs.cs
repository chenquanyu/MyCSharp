using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility.Utility
{
    public class RestHelper
    {
        private readonly RestClient httpClient;

        public RestHelper(string url)
        {
            httpClient = new RestClient(url);
        }

        public async Task<T> SendAsync<T>(object request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var req = new RestRequest(Method.POST);
            req.AddJsonBody(requestJson);

            var result = await httpClient.ExecutePostTaskAsync(req);
            var content = result.Content;
            var response = JsonConvert.DeserializeObject<T>(content);
            return response;
        }

        public T Send<T>(object request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var req = new RestRequest(Method.POST);
            req.AddJsonBody(requestJson);

            var result = httpClient.Post(req);
            var content = result.Content;
            var response = JsonConvert.DeserializeObject<T>(content);
            return response;
        }

    }
}
