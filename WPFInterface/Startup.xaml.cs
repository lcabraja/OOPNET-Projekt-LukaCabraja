using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFInterface
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        // ===================================================================================== Props & Variables
        internal bool keepAlive = false;
        private UserSettings.Language cachedLanguage;
        private bool isLanguagePicked = false;
        private UserSettings.League cachedRepresentation;
        private bool isRepresentationPicked = false;
        private bool isHomePicked = false;
        private bool isGuestPicked = false;
        private int cachedScreensize;
        private bool isScreenPicked = false;
        // ===================================================================================== Constructor & Loaded
        public Startup()
        {
            InitializeComponent();
            Loaded += Startup_Loaded;
            Closing += Startup_Closing;

            btContinue.Click += btContinue_Click;

            rbLangCro.Checked += rbLang_Checked;
            rbLangEng.Checked += rbLang_Checked;

            rbRepFem.Checked += rbRep_Checked;
            rbRepMale.Checked += rbRep_Checked;

            rbSizeFullscreen.Checked += rbResolution_Checked;
            rbSize720p.Checked += rbResolution_Checked;
            rbSize480p.Checked += rbResolution_Checked;
            rbSize360p.Checked += rbResolution_Checked;

            cbRepresentation.SelectionChanged += CbRepresentation_SelectionChanged;
            cbRepresentationGuest.SelectionChanged += CbRepresentationGuest_SelectionChanged;
        }
        private void Startup_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeLocals();
        }
        // ===================================================================================== Language Radio 
        private void rbLang_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioChecked)
            {
                if (radioChecked == rbLangEng)
                {
                    cachedLanguage = UserSettings.Language.English;
                    rbLangCro.IsChecked = false;
                }
                else if (radioChecked == rbLangCro)
                {
                    cachedLanguage = UserSettings.Language.Croatian;
                    rbLangEng.IsChecked = false;
                }
                isLanguagePicked = true;
                AdvanceLevel(0);
            }
        }
        // ===================================================================================== Representation Radio 
        private void rbRep_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioChecked)
            {
                if (radioChecked == rbRepFem)
                {
                    cachedRepresentation = UserSettings.League.Female;
                    rbRepMale.IsChecked = false;
                }
                else if (radioChecked == rbRepMale)
                {
                    cachedRepresentation = UserSettings.League.Male;
                    rbRepFem.IsChecked = false;
                }
                isRepresentationPicked = true;
                AdvanceLevel(0);
                FillHomeComboBox();
            }
        }
        // ===================================================================================== ComboBox Handlers
        private async void CbRepresentation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fifaCodeHome = DetermineSelectedFifaCode(cbRepresentation);

            List<Match> matches = await GetMatches(fifaCodeHome);
            List<string> otherReps = new List<string>();
            matches.Where(x => x.HomeTeam.Code == fifaCodeHome).ToList()
                .ForEach(x => otherReps.Add($"{x.AwayTeam.Country} ({x.AwayTeam.Code})"));
            matches.Where(x => x.AwayTeam.Code == fifaCodeHome).ToList()
                .ForEach(x => otherReps.Add($"{x.HomeTeam.Country} ({x.HomeTeam.Code})"));
            cbRepresentationGuest.ItemsSource = otherReps;
            AdvanceLevel(1);
            isHomePicked = true;
        }
        private async void CbRepresentationGuest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fifaCodeHome = DetermineSelectedFifaCode(cbRepresentation);
            string fifaCodeGuest = DetermineSelectedFifaCode(cbRepresentationGuest);
            lbScore.Content = "Fetching";
            List<Match> matches = await GetMatches(fifaCodeHome);
            lbScore.Content = "Done";
            Match match1 = matches.Where(x => x.HomeTeam.Code == fifaCodeHome && x.AwayTeam.Code == fifaCodeGuest).FirstOrDefault();
            Match match2 = matches.Where(x => x.HomeTeam.Code == fifaCodeGuest && x.AwayTeam.Code == fifaCodeHome).FirstOrDefault();
            Match match = match1 ?? match2;
            lbScore.Content = $"{match.HomeTeam.Code} {match.HomeTeam.Goals} : {match.AwayTeam.Code} {match.AwayTeam.Goals}";
            AdvanceLevel(1);
            isGuestPicked = true;
        }
        // ===================================================================================== Radio Screen Size
        private void rbResolution_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioChecked)
            {
                if (radioChecked == rbSizeFullscreen)
                {
                    cachedScreensize = 0;
                    UncheckSizeButtons(rbSizeFullscreen);
                }
                else if (radioChecked == rbSize360p)
                {
                    cachedScreensize = 10;
                    UncheckSizeButtons(rbSize360p);
                }
                else if (radioChecked == rbSize480p)
                {
                    cachedScreensize = 20;
                    UncheckSizeButtons(rbSize480p);
                }
                else if (radioChecked == rbSize720p)
                {
                    cachedScreensize = 30;
                    UncheckSizeButtons(rbSize720p);
                }
                isScreenPicked = true;
                AdvanceLevel(1);
            }
        }
        private void UncheckSizeButtons(RadioButton button)
        {
            if (button != rbSizeFullscreen)
                rbSizeFullscreen.IsChecked = false;
            if (button != rbSize360p)
                rbSize360p.IsChecked = false;
            if (button != rbSize480p)
                rbSize480p.IsChecked = false;
            if (button != rbSize720p)
                rbSize720p.IsChecked = false;
        }
        // ===================================================================================== Lower Half
        private async void FillHomeComboBox()
        {
            List<TeamResult> representations = (await GetRepresentations()).OrderBy(x => x.FifaCode).ToList();
            cbRepresentation.ItemsSource = representations.Select(x => $"{x.Country} ({x.FifaCode})").ToList();
            if (App.userSettings.SavedLeague == cachedRepresentation && App.lastTeam != null)
            {
                cbRepresentation.SelectedValue = $"{App.lastTeam.Country} ({App.lastTeam.FifaCode})";
            }
            else
            {
                cbRepresentation.SelectedIndex = 0;
            }
        }
        private string DetermineSelectedFifaCode(ComboBox comboBox)
        {
            return comboBox.SelectedValue.ToString().Split("(").Last().Substring(0, 3);
        }
        // ===================================================================================== Request Matches
        private async Task<List<Match>> GetMatches(string fifa_code)
        {
            List<Match> matches = null;
            string baseurl = GetGenderedCachedRepresentation();
            string uri = App.CACHE + baseurl.Substring(7).Replace('\\', '-').Replace('/', '-') + fifa_code + ".json";
            if (File.Exists(uri))
            {
                matches = await Fetch.FetchJsonFromFileAsync<List<Match>>(uri);
            }
            else
            {
                matches = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(baseurl, fifa_code));
                File.WriteAllText(uri, JsonConvert.SerializeObject(matches));
            }
            return matches;
        }
        // ===================================================================================== Request Team Results
        private async Task<List<TeamResult>> GetRepresentations()
        {
            List<TeamResult> representations = null;
            string baseUrl = GetGenderedCachedRepresentation();

            string uri = App.CACHE + baseUrl.Substring(7).Replace('\\', '-').Replace('/', '-') + "TeamResult" + ".json";
            if (File.Exists(uri))
            {
                representations = await Fetch.FetchJsonFromFileAsync<List<TeamResult>>(uri);
            }
            else
            {
                representations = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(baseUrl));
                File.WriteAllText(uri, JsonConvert.SerializeObject(representations));
            }
            return representations;
        }
        // ===================================================================================== Get Gendered Base URL
        private string GetGenderedCachedRepresentation()
        {
            string baseUrl = null;
            if (cachedRepresentation == UserSettings.League.Female)
                baseUrl = URL.F_BASE_URL;
            if (cachedRepresentation == UserSettings.League.Male)
                baseUrl = URL.M_BASE_URL;
            return baseUrl;
        }
        // ===================================================================================== Progress Tracker
        private void AdvanceLevel(int v)
        {
            if (v == 0)
            {
                if (isLanguagePicked && isRepresentationPicked)
                {
                    spRepresentation.IsEnabled = true;
                    spScreensize.IsEnabled = true;
                }
            }
            else if (v == 1)
            {
                if (isHomePicked && isGuestPicked && isScreenPicked)
                {
                    btContinue.IsEnabled = true;
                }
            }
        }
        // ===================================================================================== Locals
        private void InitializeLocals()
        {
            this.Title = App.LocalizedString("Onboarding");
        }
        // ===================================================================================== Finish and save
        //private void finishOnboarding()
        //{
        //    UserSettings user;
        //    ScreenSize size;
        //    try
        //    {
        //        user = new UserSettings { SavedLanguage = cachedLanguage, SavedLeague = cachedRepresentation };
        //        size = new ScreenSize { ChosenSize = cachedScreensize };
        //        App.UpdateUserSettings(user);
        //        try
        //        {
        //            File.WriteAllText(App.USER, user.ToString());
        //            File.WriteAllText(App.REPRESENTATION, FindSelectedRepresentation(cbRepresentation).ToString());
        //            File.WriteAllText(App.SIZE, size.ToString());
        //            App.UpdateUserSettings(user);
        //            App.UpdateUserSize(size);
        //            App.tryFifa_code();
        //            keepAlive = true;
        //        }
        //        catch (Exception)
        //        {
        //            EndOnboarding();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        keepAlive = false;
        //        EndOnboarding();
        //    }
        //    finally
        //    {
        //        this.Close();
        //    }
        //}
        // ===================================================================================== Abort
        private static void AbortOnboarding()
        {
            MessageBox.Show(App.LocalizedString("errorOnboarding"));
            try
            {
                File.Delete(App.USER);
            }
            catch
            {
                MessageBox.Show(App.LocalizedString("errorEndOnboarding"), App.LocalizedString("userSettingsError"));
            }
            finally
            {
                Environment.Exit(0);
            }
        }
        // ===================================================================================== Continue Button
        private void btContinue_Click(object sender, EventArgs e)
        {
            if(isLanguagePicked && isRepresentationPicked && isHomePicked && isGuestPicked && isScreenPicked)
            {
                
            }
        }
        // ===================================================================================== Close app on premature setup end
        private void Startup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (keepAlive || !App.firstOnboarding) return;
            Environment.Exit(0);
        }
    }
}
