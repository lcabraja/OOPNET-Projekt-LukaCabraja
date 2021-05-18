using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public static class Fetch
    {
        public static T FetchJsonFromUrl<T>(string resourceUrl)
        {
            var client = new RestClient(resourceUrl);
            var res = client.Execute(new RestRequest()).Content;
            T data = JsonConvert.DeserializeObject<T>(res);
            return data;
        }
        public static async Task<T> FetchJsonFromUrlAsync<T>(string resourceUrl)
        {
            var client = new RestClient(resourceUrl);
            var res = await client.ExecuteAsync(new RestRequest());
            T data = JsonConvert.DeserializeObject<T>(res.Content);
            return data;
        }
        public static T FetchJsonFromFile<T>(string resoruceUri)
        {
            var sr = new StreamReader(resoruceUri);
            var iobuffer = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(iobuffer);
        }
        public static async Task<T> FetchJsonFromFileAsync<T>(string resoruceUri)
        {
            var sr = new StreamReader(resoruceUri);
            var iobuffer = await sr.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(iobuffer);
        }
    }
}