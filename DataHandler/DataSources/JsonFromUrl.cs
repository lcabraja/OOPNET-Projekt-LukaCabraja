using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler.DataSources
{
    public class JsonFromUrl : IJsonData
    {
        public string URL { get; }
        public JsonFromUrl(string url) => URL = url;
        public async Task<string> GetJsonStringDataAsync()
        {
            var client = new HttpClient();
            var page = await client.GetStringAsync(URL);
            return page;
        }
        public string GetJsonStringData()
        {
            var client = new RestClient(URL);
            return client.Execute(new RestRequest()).Content;
        }
    }
}
