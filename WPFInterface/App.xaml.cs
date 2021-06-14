using DataHandler;
using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string BASE_DIR = Path.Join(Path.GetTempPath() + "OOPNET-LC");
        public static string USER { get { return BASE_DIR + "\\user.json"; } }
        public static string SIZE { get { return BASE_DIR + "\\size.json"; } }
        public static string REPRESENTATION { get { return BASE_DIR + "\\rep.json"; } }
        public static string CACHE { get { return BASE_DIR + "\\cache\\"; } }
        public static string FEMALE_TEAMS { get { return BASE_DIR + "\\f\\"; } }
        public static string MALE_TEAMS { get { return BASE_DIR + "\\m\\"; } }
        public static UserSettings userSettings { get; set; }
        public static bool isUserOnboarded = false;
        public static ScreenSize screenSize { get; set; }
        public static bool isScreensizeSet = false;
        public static TeamResult lastTeam { get; set; }
        public static Localizer localizer { get; private set; }
        internal static bool firstOnboarding = true;
        internal static UserSettings.Language defaultLocale = UserSettings.Language.English;

        internal static string fifaCodeHome = null;
        internal static string fifaCodeGuest = null;

        void App_Startup(object sender, StartupEventArgs e)
        {
            PreparePaths();
            PrepareLocale();
            tryFifa_code();
            tryScreenSize_code();
            isUserOnboarded = userOnboarded();;

            Startup startup = new Startup();
            startup.ShowDialog();
            firstOnboarding = false;

            UpdateUserSettings();

            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        internal static void UpdateUserSettings()
        {
            UpdateUserSettings(userSettings);
        }

        internal static void UpdateUserSettings(UserSettings user)
        {
            userSettings = user;
            localizer = UpdateLocale(user.SavedLanguage);
        }

        internal static void UpdateUserSize()
        {
            UpdateUserSize(screenSize);
        }

        internal static void UpdateUserSize(ScreenSize size)
        {
            screenSize = size;
            //localizer = Update(size.SavedLanguage);
        }

        private static void PrepareLocale()
        {
            try
            {
                localizer = UpdateLocale(defaultLocale);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not initialize locales.");
            }
        }

        private static Localizer UpdateLocale(UserSettings.Language lang)
        {
            try
            {
                switch (lang)
                {
                    case UserSettings.Language.Croatian:
                        return new Localizer("hr"); ;
                    case UserSettings.Language.English:
                        return new Localizer("en"); ;
                    default:
                        return new Localizer("en");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not initialize locales.");
                Environment.Exit(1);
                return null;
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
        internal static void tryScreenSize_code()
        {
            try
            {
                screenSize = Fetch.FetchJsonFromFile<ScreenSize>(SIZE);
                isScreensizeSet = true;
            }
            catch
            {
                screenSize = null;
                isScreensizeSet = false;
            }
        }

        private static void PreparePaths()
        {
            try
            {
                Directory.CreateDirectory(BASE_DIR);
                Directory.CreateDirectory(FEMALE_TEAMS);
                Directory.CreateDirectory(MALE_TEAMS);
                Directory.CreateDirectory(CACHE);
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
                userSettings = null;
                return false;
            }
        }
        internal static string LocalizedString(string request)
        {
            return localizer.Resource(request);
        }
    }
}
