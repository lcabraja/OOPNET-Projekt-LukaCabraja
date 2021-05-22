using DataHandler;
using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WinFormsInterface
{
    static class Program
    {
        public const string BASE_DIR = "savedata";
        public static string USER { get { return BASE_DIR + "/user.json"; } }
        public static string REPRESENTATION { get { return BASE_DIR + "/rep.json"; } }
        public static string FEMALE_TEAMS { get { return BASE_DIR + "/f/"; } }
        public static string MALE_TEAMS { get { return BASE_DIR + "/m/"; } }
        public static UserSettings userSettings { get; set; }
        public static TeamResult lastTeam { get; set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PreparePaths();
            if (!userOnboarded())
            {
                Application.Run(new Onboarding());
            }
            tryFifa_code();
            Application.Run(new FavoriteRepresentation());
            Application.Run(new FavoritePlayers());
        }

        private static void tryFifa_code()
        {
            try
            {
                lastTeam = Fetch.FetchJsonFromFile<TeamResult>(REPRESENTATION);
            } catch
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
    }
}
