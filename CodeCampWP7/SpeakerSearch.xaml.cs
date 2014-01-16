using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CodeCamp.Models;
using CodeCampWP7.ViewModels;

namespace CodeCampWP7
{
    public partial class SpeakerSearch : PhoneApplicationPage
    {
        public SpeakerSearch()
        {
            DataContext = new SpeakerSearchViewModel() {
                Filter=string.Empty,
                FilteredSpeakers = App.ViewModel.EventModel.Speakers
            };
            InitializeComponent();
            App.AddWatermark(txtSearch, "search");
        }

        private void Speaker_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel speaker = (StackPanel)sender;
            App.ViewModel.SpeakerModel = (Speaker)speaker.DataContext;
            NavigationService.Navigate(new Uri("/SpeakerPage.xaml", UriKind.Relative));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SpeakerSearchViewModel)DataContext).Filter = ((TextBox)sender).Text == "search" ? string.Empty : ((TextBox)sender).Text;
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            App.RemoveWatermark(txtSearch, "search");
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            App.AddWatermark(txtSearch, "search");
        }
    }
}