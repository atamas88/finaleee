using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace LifeInEsbjergGateway.Services
{
   public class WebApiService
    {
        private WebApiService(string baseUri)
        {
            BaseUri = baseUri;
        }

        private static WebApiService _instance;

        public static WebApiService Instance
        {
            get { return _instance ?? (_instance = new WebApiService(ConfigurationManager.AppSettings["WebApiUri"])); }
        }

        public string BaseUri { get; private set; }

        public async Task<T> GetAsync<T>(string action)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(action);

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new ApiException(result.StatusCode, json);
            }
            //using (var client = new HttpClient())
            //{
            //    HttpResponseMessage response =
            //        client.GetAsync("http://localhost:17348/api/company/").Result;
            //    return response.Content.ReadAsAsync<IEnumerable<Company>>().Result;
            //}
        }
        private string BuildActionUri(string action)
        {
            return BaseUri + action;
        }
    }
}
