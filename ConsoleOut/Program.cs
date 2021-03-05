using DataHandler.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleOut
{
    class Program
    {
        public static string GetReleases(string url)
        {
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            return response.Content;
        }
        static void Main(string[] args)
        {
            string test = GetReleases("http://worldcup.sfg.io/teams/group_results");
            Console.WriteLine(test);
            Console.ReadLine();
        }
    }
}
