using DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WinFormsInterface
{
    static class Program
    {
        public const string DIR = "user.json";

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!userOnboarded())
            {
                Application.Run(new Onboarding()); 
            }
            
        }

        private static bool userOnboarded()
        {
            try
            {
                UserSettings userSettings = Fetch.FetchJsonFromFile<UserSettings>(DIR);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
