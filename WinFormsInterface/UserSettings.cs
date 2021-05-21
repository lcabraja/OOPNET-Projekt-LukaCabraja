using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsInterface
{
    public class UserSettings
    {
        public League SavedLeague { get; set; }
        public Language SavedLanguage { get; set; }

        public UserSettings(int savedLeague, int savedLanguage)
        {
            SavedLeague = (League)savedLeague;
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
        public string GenderedRepresentation()
        {
            switch (SavedLeague)
            {
                case League.Female:
                    return URL.F_BASE_URL;
                case League.Male:
                    return URL.M_BASE_URL;
                default:
                    throw new Exception("Unsupported league selected");
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
            //return $"{{ \"SavedLeague\": {SavedLeague}, \"SavedLanguage\": {SavedLanguage}}}";
        }
    }
}
