using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsInterface
{
    class UserSettings
    {
        public League SavedLeague { get; set; }
        public Language SavedLanguage { get; set; }

        public UserSettings(int savedLeague, int savedLanguage)
        {
            Console.WriteLine($"{savedLeague}, {savedLanguage}");
            SavedLeague = (League)savedLeague;
            Console.WriteLine($"{SavedLeague}, {SavedLanguage}");
            SavedLanguage = (Language)savedLanguage;
        }

        public enum League
        {
            Female = 0,
            Male = 1
        }

        public enum Language
        {
            English = 0,
            Croatian = 1
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
            //return $"{{ \"SavedLeague\": {SavedLeague}, \"SavedLanguage\": {SavedLanguage}}}";
        }
    }
}
