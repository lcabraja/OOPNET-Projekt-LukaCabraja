using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataHandler.Model
{
    public abstract class Schema
    {
        public static List<T> FromJson<T>(string json) where T : Schema => JsonConvert.DeserializeObject<List<T>>(json);
    }
}
