using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Windows.System;
using CodeCamp.Models;
using Microsoft.Phone.Shell;
using Model;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace CodeCampWP7
{
    public partial class MainPagePivot
    {
        // Constructor
        public MainPagePivot()
        {
            InitializeComponent();

            // Set the data context of the main page to the application view-model
            DataContext = App.ViewModel;
            Loaded += MainPage_Loaded;
            App.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsUpdateLoaded":
                    if (!App.ViewModel.IsUpdateLoaded)
                    {
                        var progressIndicator = new ProgressIndicator
                        {
                            IsVisible = true,
                            IsIndeterminate = true,
                            Text = "Loading"
                        };
                        SystemTray.SetProgressIndicator(this, progressIndicator);
                    }
                    else
                    {
                        var progressIndicator = new ProgressIndicator
                        {
                            IsVisible = false,
                        };
                        SystemTray.SetProgressIndicator(this, progressIndicator);
                    }
                    break;
            }
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private void Speaker_Tap(object sender, GestureEventArgs e)
        {
            var speaker = (StackPanel)sender;
            App.ViewModel.SpeakerModel = (Speaker)speaker.DataContext;
            NavigationService.Navigate(new Uri("/SpeakerPage.xaml", UriKind.Relative));
        }

        private void Session_Tap(object sender, GestureEventArgs e)
        {
            var session = (StackPanel)sender;
            App.ViewModel.SessionModel = (Session)session.DataContext;
            NavigationService.Navigate(new Uri("/SessionPage.xaml", UriKind.Relative));
        }


        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBar.Buttons.Clear();
            var pivotItem = (Sections)MainPivot.SelectedIndex;
            ApplicationBarIconButton button;
            switch (pivotItem)
            {
                case Sections.Speakers:
                    button = new ApplicationBarIconButton
                    {
                        IconUri = new Uri("/Assets/AppBar/appbar.feature.search.rest.png",UriKind.Relative),
                        Text = "Search"
                    };
                    button.Click += OnSpeakerSearch;
                    ApplicationBar.Buttons.Add(button);
                    break;
                case Sections.Sessions:
                    button = new ApplicationBarIconButton
                    {
                        IconUri = new Uri("/Assets/AppBar/appbar.feature.search.rest.png",UriKind.Relative),
                        Text = "Search"
                    };
                    button.Click += OnSessionSearch;
                    ApplicationBar.Buttons.Add(button);
                    break;
            }
            ApplicationBar.IsVisible = ApplicationBar.Buttons.Count > 0;
        }

        public enum AppBarButtons
        {
            SpeakerSearch,
            SessionSearch
        }

        private void OnSpeakerSearch(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SpeakerSearch.xaml", UriKind.Relative));
        }

        private void OnSessionSearch(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SessionSearch.xaml", UriKind.Relative));            
        }

        private async void Walking_Tap(object sender, GestureEventArgs e)
        {
            var uri = new Uri("ms-walk-to:?destination.latitude=" + App.ViewModel.EventModel.Location.Latitude.ToString(CultureInfo.InvariantCulture) +
                            "&destination.longitude=" + App.ViewModel.EventModel.Location.Longitude.ToString(CultureInfo.InvariantCulture) + "&destination.name=" + App.ViewModel.EventModel.Title);
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Driving_Tap(object sender, GestureEventArgs e)
        {
            var uri = new Uri("ms-drive-to:?destination.latitude=" + App.ViewModel.EventModel.Location.Latitude.ToString(CultureInfo.InvariantCulture) +
                            "&destination.longitude=" + App.ViewModel.EventModel.Location.Longitude.ToString(CultureInfo.InvariantCulture) + "&destination.name=" + App.ViewModel.EventModel.Title);
            await Launcher.LaunchUriAsync(uri);
        }
    }
}