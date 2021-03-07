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
         public async Task<string> GetJsonStringData()
        {
            using (StreamReader reader = new StreamReader(Filepath))
            {
                var lines = await reader.ReadToEnd();
                return lines;
            }
        }
        public string GetJsonStringData()
        {
            using (StreamReader reader = new StreamReader(Filepath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
