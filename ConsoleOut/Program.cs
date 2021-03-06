using DataHandler.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleOut
{

    public static class Download
        
    {
        public static List<T> JSONGet<T> (string url)
            where T : Schema
            //What to do with .Schema, remove it? because it isn't doing anything, I just want it to make sure that i only deserialize into my model classes
            //The main issue is being unable to inherit static methods
        {

            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            return (List<T>)typeof(T).GetMethod("FromJson").Invoke(null, new object[] { response.Content });
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var gr = Download.JSONGet<GroupResult>(GroupResult.PrimaryURL);
            gr.ForEach(t => t.COut());
            var m = Download.JSONGet<Match>(Match.PrimaryURL);
            m.ForEach(t => t.COut());
            Console.ReadLine();
        }
    }
}
