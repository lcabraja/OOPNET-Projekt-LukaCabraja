using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleOut
{
    static class ConsolePrinter
    {
        public static void PrettyPrint(string caller, string line, ConsoleColor Key, ConsoleColor value, string lineprefix = "\t")
        {
            Console.WriteLine($"{ caller} {{");

        }
        private static void LinePrint(string? prefix, string line, string? postfix = null)
        {
            Console.WriteLine(prefix + line + postfix);
        }
        public static void COut(this Team item)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(item);
            Console.ResetColor();
        }
        public static void COut(this Result item)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(item);
            Console.ResetColor();
        }
        public static void COut(this GroupResult item)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(item);
            Console.ForegroundColor = ConsoleColor.Green;
            item.OrderedTeams.ForEach(Console.WriteLine);
            Console.ResetColor();
        }

        public static void COut(this Match item)
        {
            if (item == null)
                return;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(item);
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Officials {");
            item.Officials.ForEach(x => LinePrint("\t", x));
            Console.Write("}");
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("HomeTeamEvents: ");
            item.HomeTeamEvents.ForEach(x => LinePrint("\t", x.ToString()));
            Console.Write("}");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("AwayTeamEvents: ");
            item.AwayTeamEvents.ForEach(x => LinePrint("\t", x.ToString()));
            Console.Write("}");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("HomeTeamStatistics: ");
            Console.WriteLine(item.HomeTeamStatistics);
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("AwayTeamStatistics: ");
            Console.WriteLine(item.AwayTeamStatistics);

            Console.ResetColor();
        }
    }
}
