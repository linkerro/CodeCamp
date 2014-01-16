using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CodeCamp.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace CodeCampWP7
{
    public partial class MainPagePivot : PhoneApplicationPage
    {
        // Constructor
        public MainPagePivot()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsUpdateLoaded":
                    if (!App.ViewModel.IsUpdateLoaded)
                    {
                        ProgressIndicator progressIndicator = new ProgressIndicator()
                        {
                            IsVisible = true,
                            IsIndeterminate = true,
                            Text = "Loading"
                        };
                        SystemTray.SetProgressIndicator(this, progressIndicator);
                    }
                    else
                    {
                        ProgressIndicator progressIndicator = new ProgressIndicator()
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

        private void Speaker_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel speaker = (StackPanel)sender;
            App.ViewModel.SpeakerModel = (Speaker)speaker.DataContext;
            NavigationService.Navigate(new Uri("/SpeakerPage.xaml", UriKind.Relative));
        }

        private void Session_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel session = (StackPanel)sender;
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
                    button = new ApplicationBarIconButton()
                    {
                        IconUri = new Uri("/Assets/AppBar/appbar.feature.search.rest.png",UriKind.Relative),
                        Text = "Search"
                    };
                    button.Click += OnSpeakerSearch;
                    ApplicationBar.Buttons.Add(button);
                    break;
                case Sections.Sessions:
                    button = new ApplicationBarIconButton()
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

        private ApplicationBarIconButton GetIconButton(AppBarButtons id)
        {
            return (ApplicationBarIconButton)ApplicationBar.Buttons[(int)id];
        }

        public enum Sections
        {
            Description,
            Speakers,
            Sessions,
            Tracks,
            Location
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
    }
}