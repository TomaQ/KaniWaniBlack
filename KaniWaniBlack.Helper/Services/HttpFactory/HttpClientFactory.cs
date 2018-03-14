using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KaniWaniBlack.Helper.Services.HttpFactory
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string apiKey)
        {
            var client = new HttpClient();
            SetupClientDefaults(client, apiKey);
            return client;
        }

        protected virtual void SetupClientDefaults(HttpClient client, string apiKey)
        {
            client.Timeout = TimeSpan.FromSeconds(120);
            string baseAddress = Strings.WANIKANI_API_URL + apiKey + "/vocabulary/";
            client.BaseAddress = new Uri(baseAddress);
        }
    }
}