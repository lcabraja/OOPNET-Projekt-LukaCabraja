using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private IDictionary<int, int> results;
        private bool keepAlive = false;
        private bool dataLoaded = false;
        private List<TeamResult> teams;

        private UserSettings.Language cachedLanguage;
        private bool isLanguagePicked = false;
        private UserSettings.League cachedLeague;
        private bool isRepresentationPicked = false;
        private int cachedResolution;
        private bool isScreenPicked = false;
        private bool isSelfUpdate = false;
        private bool isDataFetched = false;
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

        }
        private void Startup_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeLocals();
            if (App.userSettings == null)
            {
                cachedLanguage = App.defaultLocale;
            }
            else
            {
                cachedLanguage = App.userSettings.SavedLanguage;
                cachedLeague = App.userSettings.SavedLeague;
            }
            UpdateRadioLang();
            updateRadioRep();
        }
        // ===================================================================================== Language Radio 
        private void rbLang_Checked(object sender, RoutedEventArgs e)
        {
            if (!isSelfUpdate)
            {
                if (sender is RadioButton rbSelected)
                {
                    if (rbSelected == rbLangEng)
                    {
                        cachedLanguage = UserSettings.Language.English;
                    }
                    else
                    {
                        cachedLanguage = UserSettings.Language.Croatian;
                    }
                    UpdateRadioLang();
                    isLanguagePicked = true;
                }
            }
            isSelfUpdate = false;
        }
        private void UpdateRadioLang()
        {
            isSelfUpdate = true;
            rbLangCro.IsChecked = false;
            rbLangEng.IsChecked = false;
            if (cachedLanguage == UserSettings.Language.English)
            {

                rbLangEng.IsChecked = true;
            }
            else
            {
                rbLangCro.IsChecked = true;
            }
            isLanguagePicked = true;
            tryLowerHalf();
            isSelfUpdate = false;
        }
        // ===================================================================================== Representation Radio 
        private void rbRep_Checked(object sender, RoutedEventArgs e)
        {
            if (!isSelfUpdate)
            {
                if (sender is RadioButton rbSelected)
                {
                    if (rbSelected == rbRepFem)
                    {
                        cachedLeague = UserSettings.League.Female;
                    }
                    else
                    {
                        cachedLeague = UserSettings.League.Male;
                    }
                    updateRadioRep();
                    isRepresentationPicked = true;
                }
            }
            isSelfUpdate = false;
        }
        private void updateRadioRep()
        {
            isSelfUpdate = true;
            rbRepFem.IsChecked = false;
            rbRepMale.IsChecked = false;
            if (cachedLeague == UserSettings.League.Female)
            {
                rbRepFem.IsChecked = true;
            }
            else
            {
                rbRepMale.IsChecked = true;
            }
            isRepresentationPicked = true;
            tryLowerHalf();
            isSelfUpdate = false;
        }
        // ===================================================================================== Representation Screen Size
        private void rbResolution_Checked(object sender, RoutedEventArgs e)
        {
            if (!isSelfUpdate)
            {
                if (sender is RadioButton rbSelected)
                {
                    if (rbSelected == rbSizeFullscreen)
                    {
                        cachedResolution = 0;
                    }
                    if (rbSelected == rbSize720p)
                    {
                        cachedResolution = 10;
                    }
                    if (rbSelected == rbSize480p)
                    {
                        cachedResolution = 20;
                    }
                    if (rbSelected == rbSize360p)
                    {
                        cachedResolution = 30;
                    }
                    updateRadioResolution();
                }
            }
            isSelfUpdate = false;
        }
        private void updateRadioResolution()
        {
            isSelfUpdate = true;
            rbSizeFullscreen.IsChecked = false;
            rbSize720p.IsChecked = false;
            rbSize480p.IsChecked = false;
            rbSize360p.IsChecked = false;
            if (cachedResolution == 0)
            {
                rbSizeFullscreen.IsChecked = true;
            }
            if (cachedResolution == 10)
            {
                rbSize720p.IsChecked = true;
            }
            if (cachedResolution == 20)
            {
                rbSize480p.IsChecked = true;
            }
            if (cachedResolution == 30)
            {
                rbSize360p.IsChecked = true;
            }
            isScreenPicked = true;
            tryLowerHalf();
            isSelfUpdate = false;
        }
        // ===================================================================================== Lower Half
        private void tryLowerHalf()
        {
            if (isLanguagePicked && isRepresentationPicked)
            {
                if (App.isUserOnboarded)
                {
                    setupLowerHalf();
                }
                else
                {
                    try
                    {
                        var user = new UserSettings { SavedLanguage = cachedLanguage, SavedLeague = cachedLeague };
                        File.WriteAllText(App.USER, user.ToString());
                        App.userSettings = user;
                        App.isUserOnboarded = true;
                    }
                    catch (HttpStatusException ex)
                    {
                        lbLoadingRepresentation.Content = App.LocalizedString("Aborting");
                        MessageBox.Show(ex.Message, ex.GetType().Name);
                        Environment.Exit(0);
                    }
                    catch (JsonException ex)
                    {
                        lbLoadingRepresentation.Content = App.LocalizedString("Aborting");
                        MessageBox.Show(ex.Message, ex.GetType().Name);
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        lbLoadingRepresentation.Content = App.LocalizedString("Aborting");
                        MessageBox.Show(ex.Message, ex.GetType().Name);
                        Environment.Exit(0);
                    }
                }
            }
        }
        private async void setupLowerHalf()
        {
            spRepresentation.IsEnabled = true;
            spScreensize.IsEnabled = true;
            if (!isDataFetched)
            {
                lbLoadingRepresentation.Content = App.LocalizedString("Fetching");
                try
                {
                    var representations = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(App.userSettings.GenderedRepresentationUrl()));

                    cachedResolution = App.screenSize.ChosenSize;
                    updateRadioResolution();

                    teams = representations;
                    cbRepresentation.ItemsSource = representations.OrderBy(x => x.FifaCode)
                                                                  .Select(x => $"{x.Country} ({x.FifaCode})")
                                                                  .ToList();
                    dataLoaded = true;
                    lbLoadingRepresentation.Content = App.LocalizedString("Done");
                    if (App.lastTeam != null)
                    {
                        cbRepresentation.SelectedItem =
                            $"{App.lastTeam.Country} ({App.lastTeam.FifaCode})";
                    }
                    isDataFetched = true;

                }
                catch (HttpStatusException ex)
                {
                    lbLoadingRepresentation.Content = App.LocalizedString("Aborting");
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                    Environment.Exit(0);
                }
                catch (JsonException ex)
                {
                    lbLoadingRepresentation.Content = App.LocalizedString("Aborting");
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    lbLoadingRepresentation.Content = App.LocalizedString("Aborting");
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                    Environment.Exit(0);
                }
            }
        }
        // ===================================================================================== Locals
        private void InitializeLocals()
        {
            this.Title = App.LocalizedString("Onboarding");
        }
        // ===================================================================================== Finish and save
        private void finishOnboarding()
        {
            UserSettings user;
            ScreenSize size;
            try
            {
                user = new UserSettings { SavedLanguage = cachedLanguage, SavedLeague = cachedLeague };
                size = new ScreenSize { ChosenSize = cachedResolution };
                App.UpdateUserSettings(user);
                try
                {
                    File.WriteAllText(App.USER, user.ToString());
                    File.WriteAllText(App.REPRESENTATION, FindSelectedRepresentation());
                    File.WriteAllText(App.SIZE, size.ToString());
                    App.UpdateUserSettings(user);
                    App.UpdateUserSize(size);
                    App.tryFifa_code();
                    keepAlive = true;
                }
                catch (Exception)
                {
                    EndOnboarding();
                }
            }
            catch (Exception)
            {
                keepAlive = false;
                EndOnboarding();
            }
            finally
            {
                this.Close();
            }
        }
        // ===================================================================================== Abort
        private static void EndOnboarding()
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
            if (isLanguagePicked && isRepresentationPicked && isScreenPicked)
            {
                finishOnboarding();
            }
        }
        private string FindSelectedRepresentation()
        {
            var fifa_code = cbRepresentation.SelectedItem.ToString()
                                                         .Split(' ')
                                                         .Last()
                                                         .Substring(1, 3);

            return teams.Find(x => x.FifaCode == fifa_code).ToString();
        }
        // ===================================================================================== Close app on premature setup end
        private void Startup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //base.OnClosing(e); pretty sure this is a recursion here

            if (keepAlive || !App.firstOnboarding) return;

            // Application.Exit() did in fact not exit the application, only the form.
            Environment.Exit(0);
        }
        //private void onboarding_KeyUp(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Escape:
        //            if (!App.firstOnboarding)
        //                keepAlive = true;
        //            this.Close();
        //            break;
        //        case Keys.Enter:
        //            nextStep();
        //            break;
        //    }
        //}
    }
}
