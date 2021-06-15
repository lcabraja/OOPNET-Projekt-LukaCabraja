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
        private string teamCodeHomeFemale = null;
        private string teamCodeHomeMale = null;
        private bool isHomePicked = false;
        private string teamCodeGuestFemale = null;
        private string teamCodeGuestMale = null;
        private bool isGuestPicked = false;
        private int cachedScreensize;
        private bool isScreenPicked = false;
        private bool isSettings = false;
        private bool isSwitch = true;
        private bool isSwitchGuest = true;
        // ===================================================================================== Constructor & Loaded
        public Startup(bool isSettings = false)
        {
            this.isSettings = isSettings;
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
        // ===================================================================================== Loaded
        private void Startup_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeLocals();
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            if (App.userSettings != null)
            {
                switch (App.userSettings.SavedLanguage)
                {
                    case UserSettings.Language.English:
                        rbLangEng.IsChecked = true;
                        break;
                    case UserSettings.Language.Croatian:
                        rbLangCro.IsChecked = true;
                        break;
                    default:
                        rbLangEng.IsChecked = true;
                        break;
                }
                switch (App.userSettings.SavedLeague)
                {
                    case UserSettings.League.Female:
                        rbRepFem.IsChecked = true;
                        break;
                    case UserSettings.League.Male:
                        rbRepMale.IsChecked = true;
                        break;
                    default:
                        rbRepFem.IsChecked = true;
                        break;
                }
            }
            if (App.screenSize != null)
            {
                switch (App.screenSize.ChosenSize)
                {
                    case 0:
                        rbSizeFullscreen.IsChecked = true;
                        break;
                    case 10:
                        rbSize360p.IsChecked = true;
                        break;
                    case 20:
                        rbSize480p.IsChecked = true;
                        break;
                    case 30:
                        rbSize720p.IsChecked = true;
                        break;
                    default:
                        rbSize720p.IsChecked = true;
                        break;
                }
            }
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
                InitializeLocals();
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
                isSwitch = true;
                FillHomeComboBox();
            }
        }
        // ===================================================================================== ComboBox Handlers
        private void CbRepresentation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGuestReps();
        }

        private async void UpdateGuestReps()
        {
            var fifaCodeHome = DetermineSelectedFifaCode(cbRepresentation);
            if (cachedRepresentation == UserSettings.League.Female)
            {
                if (isSwitch)
                    fifaCodeHome = teamCodeHomeFemale ?? App.fifaCodeHomeFemale ?? fifaCodeHome;
                teamCodeHomeFemale = fifaCodeHome;
            }
            else
            {
                if (isSwitch)
                    fifaCodeHome = teamCodeHomeMale ?? App.fifaCodeHomeMale ?? fifaCodeHome;
                teamCodeHomeMale = fifaCodeHome;
            }
            isSwitch = false;
            if (fifaCodeHome == null)
            {
                isHomePicked = false;
                cbRepresentationGuest.SelectedItem = null;
                return;
            }
            lbLoadingRepresentation.Content = App.LocalizedString("Fetching");
            List<Match> matches = await GetMatches(fifaCodeHome);
            if (matches == null)
            {
                lbLoadingRepresentation.Content = App.LocalizedString("error");
                return;
            }
            lbLoadingRepresentation.Content = App.LocalizedString("Done");
            if (matches == null)
                return;
            List<string> otherReps = new List<string>();
            matches.Where(x => x.HomeTeam.Code == fifaCodeHome).ToList()
                .ForEach(x => otherReps.Add($"{x.AwayTeam.Country} ({x.AwayTeam.Code})"));
            matches.Where(x => x.AwayTeam.Code == fifaCodeHome).ToList()
                .ForEach(x => otherReps.Add($"{x.HomeTeam.Country} ({x.HomeTeam.Code})"));
            cbRepresentationGuest.ItemsSource = otherReps;
            string fifaCodeGuest = null;
            if (cachedRepresentation == UserSettings.League.Female)
            {
                if (isSwitchGuest)
                    fifaCodeGuest = teamCodeGuestFemale ?? App.fifaCodeGuestFemale ?? fifaCodeGuest;
                teamCodeGuestFemale = fifaCodeGuest;
            }
            else
            {
                if (isSwitchGuest)
                    fifaCodeGuest = teamCodeGuestMale ?? App.fifaCodeGuestMale ?? fifaCodeGuest;
                teamCodeGuestMale = fifaCodeGuest;
            }
            isSwitchGuest = false;
            if (fifaCodeGuest != null)
            {
                ItemCollection items = cbRepresentationGuest.Items;
                foreach (string item in items)
                {
                    if (item.Contains(fifaCodeGuest))
                    {
                        cbRepresentationGuest.SelectedItem = item;
                        isGuestPicked = true;
                        break;
                    }
                }
            }
            await SetMatchResults();
            AdvanceLevel(1);
            isHomePicked = true;
        }

        private async void CbRepresentationGuest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await SetMatchResults();
            isSwitchGuest = false;
        }

        private async Task SetMatchResults()
        {
            string fifaCodeHome = DetermineSelectedFifaCode(cbRepresentation);
            string fifaCodeGuest = DetermineSelectedFifaCode(cbRepresentationGuest);
            if (cachedRepresentation == UserSettings.League.Female)
            {
                if (isSwitchGuest)
                    fifaCodeHome = teamCodeHomeFemale ?? App.fifaCodeHomeFemale ?? fifaCodeHome;
                teamCodeHomeFemale = fifaCodeHome;
            }
            else
            {
                if (isSwitchGuest)
                    fifaCodeHome = teamCodeHomeMale ?? App.fifaCodeHomeMale ?? fifaCodeHome;
                teamCodeHomeMale = fifaCodeHome;
            }
            if (cachedRepresentation == UserSettings.League.Female)
            {
                if (isSwitchGuest)
                    fifaCodeGuest = teamCodeGuestFemale ?? App.fifaCodeGuestFemale ?? fifaCodeGuest;
                teamCodeGuestFemale = fifaCodeGuest;
            }
            else
            {
                if (isSwitchGuest)
                    fifaCodeGuest = teamCodeGuestMale ?? App.fifaCodeGuestMale ?? fifaCodeGuest;
                teamCodeGuestMale = fifaCodeGuest;
            }
            if (fifaCodeHome == null)
            {
                lbLoadingRepresentation.Content = null;
                isHomePicked = false;
                AdvanceLevel(1);
                return;
            }
            if (fifaCodeGuest == null)
            {
                lbLoadingRepresentationGuest.Content = null;
                isGuestPicked = false;
                AdvanceLevel(1);
                return;
            }
            lbLoadingRepresentationGuest.Content = App.LocalizedString("Fetching");
            List<Match> matches = await GetMatches(fifaCodeHome);
            if (matches == null)
            {
                lbLoadingRepresentationGuest.Content = App.LocalizedString("error");
                return;
            }
            lbLoadingRepresentationGuest.Content = App.LocalizedString("Done");
            Match match1 = matches.Where(x => x.HomeTeam.Code == fifaCodeHome && x.AwayTeam.Code == fifaCodeGuest).FirstOrDefault();
            Match match2 = matches.Where(x => x.HomeTeam.Code == fifaCodeGuest && x.AwayTeam.Code == fifaCodeHome).FirstOrDefault();
            Match match = match1 ?? match2;
            if (match == null)
            {
                lbLoadingRepresentationGuest.Content = App.LocalizedString("error");
                return;
            }
            lbLoadingRepresentationGuest.Content = $"{match.HomeTeam.Code} {match.HomeTeam.Goals} : {match.AwayTeam.Code} {match.AwayTeam.Goals}";
            isGuestPicked = true;
            AdvanceLevel(1);
        }

        // ===================================================================================== Lower Half
        private async void FillHomeComboBox()
        {
            cbRepresentation.SelectionChanged -= CbRepresentation_SelectionChanged;
            List<TeamResult> representations = (await GetRepresentations()).OrderBy(x => x.FifaCode).ToList();
            cbRepresentation.ItemsSource = representations.Select(x => $"{x.Country} ({x.FifaCode})").ToList();
            string fifaCodeHome = null;
            if (cachedRepresentation == UserSettings.League.Female)
            {
                if (isSwitch)
                    fifaCodeHome = teamCodeHomeFemale ?? App.fifaCodeHomeFemale ?? fifaCodeHome;
                teamCodeHomeFemale = fifaCodeHome;
            }
            else
            {
                if (isSwitch)
                    fifaCodeHome = teamCodeHomeMale ?? App.fifaCodeHomeMale ?? fifaCodeHome;
                teamCodeHomeMale = fifaCodeHome;
            }
            isSwitch = false;
            cbRepresentation.SelectionChanged += CbRepresentation_SelectionChanged;
            if (fifaCodeHome != null)
            {
                ItemCollection items = cbRepresentation.Items;
                foreach (string item in items)
                {
                    if (item.Contains(fifaCodeHome))
                    {
                        cbRepresentation.SelectedItem = item;
                        isHomePicked = true;
                        break;
                    }
                }
            }
            else
            {
                cbRepresentation.SelectedItem = null;
            }
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
        private string DetermineSelectedFifaCode(ComboBox comboBox)
        {
            return comboBox.SelectedValue?.ToString().Split("(").Last().Substring(0, 3);
        }
        // ===================================================================================== Request Matches
        private async Task<List<Match>> GetMatches(string fifa_code)
        {
            List<Match> matches = null;
            string baseurl = GetGenderedCachedRepresentation();
            string uri = App.CACHE + baseurl.Substring(7).Replace('\\', '-').Replace('/', '-') + fifa_code + ".json";
            try
            {
                if (File.Exists(uri))
                {
                    matches = await Fetch.FetchJsonFromFileAsync<List<Match>>(uri);
                }
                else
                {
                    matches = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(baseurl, fifa_code));
                    File.WriteAllText(uri, JsonConvert.SerializeObject(matches));
                }
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(App.LocalizedString("tooManyRequestsMessage"), App.LocalizedString("tooManyRequests"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.LocalizedString("errorExit"));
                throw;
            }
            return matches;
        }
        // ===================================================================================== Request Team Results
        private async Task<List<TeamResult>> GetRepresentations()
        {
            List<TeamResult> representations = null;
            string baseUrl = GetGenderedCachedRepresentation();
            string uri = App.CACHE + baseUrl.Substring(7).Replace('\\', '-').Replace('/', '-') + "@TeamResult" + ".json";
            try
            {
                if (File.Exists(uri))
                {
                    representations = await Fetch.FetchJsonFromFileAsync<List<TeamResult>>(uri);
                }
                else
                {
                    representations = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(baseUrl));
                    File.WriteAllText(uri, JsonConvert.SerializeObject(representations));
                }
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(App.LocalizedString("tooManyRequests"), App.LocalizedString("tooManyRequestsMessage"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, App.LocalizedString("errorExit"));
                throw;
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
                else
                {
                    btContinue.IsEnabled = false;
                }
            }
        }
        // ===================================================================================== Locals
        private void InitializeLocals()
        {
            this.Title = App.LocalizedString("Onboarding", cachedLanguage);

            this.lblang.Content = App.LocalizedString("Language", cachedLanguage);

            this.lbChampion.Content = App.LocalizedString("Championship", cachedLanguage);
            this.rbRepFem.Content = App.LocalizedString("Female", cachedLanguage);
            this.rbRepMale.Content = App.LocalizedString("Male", cachedLanguage);

            this.labelrep.Content = App.LocalizedString("Representation", cachedLanguage);
            this.labelrepguest.Content = App.LocalizedString("GuestRepresentation", cachedLanguage);

            this.label4.Content = App.LocalizedString("screensize", cachedLanguage);
            this.rbSizeFullscreen.Content = App.LocalizedString("Fullscreen", cachedLanguage);

            this.btContinue.Content = App.LocalizedString("Continue", cachedLanguage);
        }
        // ===================================================================================== Finish and save
        private async void finishOnboarding()
        {
            var lastTeam = (await GetRepresentations()).Find(x => x.FifaCode == (cachedRepresentation == UserSettings.League.Female ? teamCodeHomeFemale : teamCodeHomeMale));
            UserSettings user = new UserSettings { SavedLanguage = cachedLanguage, SavedLeague = cachedRepresentation };
            ScreenSize size = new ScreenSize { ChosenSize = cachedScreensize };
            App.UpdateUserSize(size);
            try
            {

                File.WriteAllText(App.USER, new UserSettings { SavedLanguage = cachedLanguage, SavedLeague = cachedRepresentation }.ToString());
                App.UpdateUserSettings(user);
                File.WriteAllText(App.REPRESENTATION, lastTeam.ToString());
                App.TryFifa_code();
                File.WriteAllText(App.SIZE, size.ToString());
                App.TryScreenSize();
                File.WriteAllText(
                    App.CODES,
                    new TeamCodes
                    {
                        HomeTeamCodeFemale = teamCodeHomeFemale,
                        HomeTeamCodeMale = teamCodeHomeMale,
                        GuestTeamCodeFemale = teamCodeGuestFemale,
                        GuestTeamCodeMale = teamCodeGuestMale
                    }.ToString());
                App.TryCodes();
                keepAlive = true;
            }
            catch (Exception)
            {
                keepAlive = false;
                AbortOnboarding();
            }
            finally
            {
                this.Close();
            }
        }
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
            if (isLanguagePicked && isRepresentationPicked && isHomePicked && isGuestPicked && isScreenPicked)
            {
                if (isSettings)
                {
                    Confirm confirm = new Confirm();
                    confirm.btOK.Click += BtOK_Click;
                    confirm.ShowDialog();

                    void BtOK_Click(object sender, RoutedEventArgs e)
                    {
                        finishOnboarding();
                        confirm.Close();
                    }
                }
                else
                {
                    finishOnboarding();
                }
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
