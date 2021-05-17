using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataHandler.DataSources
{
    public class JsonFromFile : IJsonData
    {
        public string Filepath { get; }
        public JsonFromFile(string filepath) => Filepath = filepath;

        public async Task<string> GetJsonStringDataAsync()
        {
            using (StreamReader sr = new StreamReader(Filepath))
            {
                return await sr.ReadToEndAsync();
            }
        }
        public string GetJsonStringData()
        {
            using (StreamReader sr = new StreamReader(Filepath))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
}
