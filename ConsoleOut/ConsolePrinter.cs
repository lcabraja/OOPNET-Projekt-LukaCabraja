using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleOut
{
    static class ConsolePrinter
    {
        public static void PrettyPrint(string caller, string line, ConsoleColor key = ConsoleColor.Blue, ConsoleColor value = ConsoleColor.Magenta, ConsoleColor foreground = ConsoleColor.White, string lineprefix = "\t")
        {
            Console.ForegroundColor = foreground;
            Console.WriteLine($"\n{caller} {{");
            List<string> lines = line.Split(", ").ToList();
            int maxlen = 0;
            lines.ForEach(x => LongestMember(x.Split(": ")[0] + ':', ref maxlen));
            maxlen = (int)Math.Ceiling((float)maxlen / 8.0);
            lines.ForEach(x => {
                if (x.Split(": ").Length < 2)
                    return;
                Console.ForegroundColor = key;
                Console.Write($"{lineprefix}{x.Split(": ")[0]}:{new string('\t', maxlen - (int)Math.Floor((float)(x.Split(": ")[0].Length + 1) / 8.0))}");
                Console.ForegroundColor = value;
                Console.WriteLine(x.Split(": ")[1]);
                });
            Console.ForegroundColor = foreground;
            Console.WriteLine("}");
            Console.ResetColor();
        }

        public static void LinePrint(string content, string prefix = "\t", string postfix = null)
            => Console.WriteLine($"{prefix}{content}{postfix}");

        public static void BR()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(new string('■', Console.WindowWidth));
            Console.ResetColor();
        }
        private static void LongestMember(string member, ref int maxlen)
        {
            if (member.Length > maxlen)
                maxlen = member.Length;
        }

        public static void COut(this Team item)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrettyPrint("Team", item.ToString(), foreground: ConsoleColor.Red);
            Console.ResetColor();
        }
        public static void COut(this Result item)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrettyPrint("Result", item.ToString(), foreground: ConsoleColor.Yellow);
            Console.ResetColor();
        }
        public static void COut(this GroupResult item)
        {
            PrettyPrint("GroupResult", item.ToString(), foreground: ConsoleColor.DarkYellow);
            item.OrderedTeams.ForEach(x => PrettyPrint("OrderedTeam", x.ToString(), foreground: ConsoleColor.DarkYellow));
            Console.ResetColor();
        }

        public static void COut(this Match item)
        {
            if (item == null)
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrettyPrint("Match", item.ToString(), foreground: ConsoleColor.Cyan);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Officials {");
            Console.ForegroundColor = ConsoleColor.Magenta;
            item.Officials.ForEach(x => LinePrint(x));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine('}');

            
            item.HomeTeamEvents.ForEach(x => PrettyPrint("HomeTeamEvents", x.ToString(), foreground: ConsoleColor.Cyan));

            item.AwayTeamEvents.ForEach(x => PrettyPrint("AwayTeamEvents", x.ToString(), foreground: ConsoleColor.Cyan));

            PrettyPrint("HomeTeamStatistics", item.ToString(), foreground: ConsoleColor.Cyan);

            PrettyPrint("AwayTeamStatistics", item.ToString(), foreground: ConsoleColor.Cyan);

            Console.ResetColor();
        }
    }
}
