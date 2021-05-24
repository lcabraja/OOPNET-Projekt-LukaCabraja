using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataHandler
{
    public class Localizer
    {
        JObject locales;
        string currentLocale;
        public Localizer(string locale)
        {
            var jsonData = File.ReadAllText("locale.json");
            locales = JObject.Parse(jsonData);
            currentLocale = locale;
        }
        public string Resource(string request)
        {
            return Resource(request, currentLocale);
        }
        public string Resource(string request, string locale)
        {
            try
            {
                return locales[request][locale].ToString();
            }
            catch (Exception)
            {
                return "MISSING STRING";
            }
        }
    }
}
