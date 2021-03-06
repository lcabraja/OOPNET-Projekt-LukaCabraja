using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandler.Model
{
    public partial class Team
    {
        public static List<Team> FromJson(string json) => JsonConvert.DeserializeObject<List<Team>>(json, Converter.Settings);
    }
    public partial class Result
    {
        public static List<Result> FromJson(string json) => JsonConvert.DeserializeObject<List<Result>>(json, Converter.Settings);
    }
    public partial class GroupResult
    {
        public static List<GroupResult> FromJson(string json) => JsonConvert.DeserializeObject<List<GroupResult>>(json, Converter.Settings);
    }
    public partial class Match
    {
        public static List<Match> FromJson(string json) => JsonConvert.DeserializeObject<List<Match>>(json, Converter.Settings);
    }
}
