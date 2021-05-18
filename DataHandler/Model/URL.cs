using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandler.Model
{
    public static class URL
    {
        public static string F_BASE_URL = "https://worldcup.sfg.io";
        public static string M_BASE_URL = "https://world-cup-json-2018.herokuapp.com";

        public static string Matches(string baseURL) 
            => baseURL + Match.ENDPOINT;

        public static string MatchesFiltered(string baseURL, string fifa_code) 
            => Matches(baseURL) + Match.FILTER + fifa_code;

        public static string Teams(string baseURL) 
            => baseURL + TeamResult.ENDPOINT;
    }
}
