using DataHandler.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using DataHandler.DataSources;

namespace ConsoleOut
{

    public static class Download
        
    {
        public static List<T> JSONGet<T> (string url)
            where T : Schema
            //What to do with .Schema, remove it? because it isn't doing anything, I just want it to make sure that i only deserialize into my model classes
            //The issue stems from my static architecture being uninheritable
        {
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            return (List<T>)typeof(T).GetMethod("FromJson", BindingFlags.Public | BindingFlags.Static).Invoke(null, new object[] { response.Content });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //JsonFromFile("");
            //JsonFromFile output = new JsonFromFile();
            //string bonjourno = output.GetJsonSt
            //ringData(@"C:\Users\doss\source\repos\OOPNET-Projekt-LukaCabraja\DataHandler\JSON\men\group_results.json");
            BeginExecution();
        }
        static void BeginExecution()
        {
            Console.Write("Press any key to begin.");
            Console.ReadKey();
            Console.Clear();
            IList<Team> teams; IList<Result> results; IList<GroupResult> groupresults; IList<Match> matches;
            DefaultPull(out teams, out results, out groupresults, out matches);
            Loop(ref teams, ref results, ref groupresults, ref matches);
        }
        static object DefaultPull(out IList<Team> teams, out IList<Result> results, out IList<GroupResult> groupresults, out IList<Match> matches)
        {
            teams = Download.JSONGet<Team>(Team.PrimaryURL);
            results = Download.JSONGet<Result>(Result.PrimaryURL);
            groupresults = Download.JSONGet<GroupResult>(GroupResult.PrimaryURL);
            matches = Download.JSONGet<Match>(Match.PrimaryURL);
            return new { teams, results, groupresults, matches };
        }

        static void Loop(ref IList<Team> teams, ref IList<Result> results, ref IList<GroupResult> groupresults, ref IList<Match> matches)
        {
            while (true)
            {
                ConsolePrinter.BR();
                Console.Write("Input (Team | Result | GroupResult | Match): ");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "team":
                        Console.WriteLine($"Team.Count == {teams.Count}");
                        Console.Write($"Enter number between {0} and {teams.Count - 1}: ");
                        int.TryParse(Console.ReadLine(), out int index1);
                        teams[index1].COut();
                        break;
                    case "result":
                        Console.WriteLine($"Result.Count == {results.Count}");
                        Console.Write($"Enter number between {0} and {results.Count - 1}: ");
                        int.TryParse(Console.ReadLine(), out int index2);
                        results[index2].COut();
                        break;
                    case "groupresult":
                        Console.WriteLine($"GroupResult.Count == {groupresults.Count}");
                        Console.Write($"Enter number between {0} and {groupresults.Count - 1}: ");
                        int.TryParse(Console.ReadLine(), out int index3);
                        groupresults[index3].COut();
                        break;
                    case "match":
                        Console.WriteLine($"Match.Count == {matches.Count}");
                        Console.Write($"Enter number between {0} and {matches.Count - 1}: ");
                        int.TryParse(Console.ReadLine(), out int index4);
                        matches[index4].COut();
                        break;
                    default:
                        Console.WriteLine("Incorrect input format... Try again");
                        break;
                }
                Console.Write("\nPress any key to continue... ");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
