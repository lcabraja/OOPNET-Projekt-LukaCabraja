﻿using DataHandler;
using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace WPFInterface
{
    class CommonFileUtils
    {
        public const string BASE_DIR = "savedata";
        public static string USER { get { return BASE_DIR + "/user.json"; } }
        public static string REPRESENTATION { get { return BASE_DIR + "/rep.json"; } }
        public static string FEMALE_TEAMS { get { return BASE_DIR + "/f/"; } }
        public static string MALE_TEAMS { get { return BASE_DIR + "/m/"; } }
        public static UserSettings userSettings { get; set; }
        public static TeamResult lastTeam { get; set; }
        public static Localizer localizer { get; private set; }
        internal static bool firstOnboarding = true;
        internal static string defaultLocale = "en";
        internal static void UpdateUser(UserSettings user)
        {
            userSettings = user;
            UpdateLocale();
        }

        private static void PrepareLocale()
        {
            try
            {
                localizer = new Localizer(defaultLocale);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not initialize locales.");
            }
        }

        internal static void UpdateLocale()
        {
            try
            {
                switch (userSettings.SavedLanguage)
                {
                    case UserSettings.Language.Croatian:
                        localizer = new Localizer("hr");
                        return;
                    case UserSettings.Language.English:
                        localizer = new Localizer("en");
                        return;
                    default:
                        localizer = new Localizer("en");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not initialize locales.");
            }
        }

        internal static void tryFifa_code()
        {
            try
            {
                lastTeam = Fetch.FetchJsonFromFile<TeamResult>(REPRESENTATION);
            }
            catch
            {
                lastTeam = null;
            }
        }

        private static void PreparePaths()
        {
            try
            {
                Directory.CreateDirectory(BASE_DIR);
                Directory.CreateDirectory(FEMALE_TEAMS);
                Directory.CreateDirectory(MALE_TEAMS);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not create save folder.");
            }
        }

        private static bool userOnboarded()
        {
            try
            {
                userSettings = Fetch.FetchJsonFromFile<UserSettings>(USER);
                return true;
            }
            catch
            {
                return false;
            }
        }
        internal static string LocalizedString(string request)
        {
            return localizer.Resource(request);
        }
    }
    }
}
