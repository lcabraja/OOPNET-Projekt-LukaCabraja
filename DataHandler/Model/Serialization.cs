using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandler.Model
{
    public static class Serialize
    {
        public static string ToJson(this Team[] self) => JsonConvert.SerializeObject(self, Team.Converter.Settings);
        public static string ToJson(this Result[] self) => JsonConvert.SerializeObject(self, Result.Converter.Settings);
        public static string ToJson(this GroupResult[] self) => JsonConvert.SerializeObject(self, GroupResult.Converter.Settings);
        public static string ToJson(this Match[] self) => JsonConvert.SerializeObject(self, Match.Converter.Settings);
    }
}
