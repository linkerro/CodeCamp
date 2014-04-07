using System;
using System.Windows.Controls;
using CodeCamp.Models;
using CodeCampWP7.ViewModels;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace CodeCampWP7
{
    public partial class SpeakerSearch
    {
        public SpeakerSearch()
        {
            DataContext = new SpeakerSearchViewModel
            {
                Filter=string.Empty,
                FilteredSpeakers = App.ViewModel.EventModel.Speakers
            };
            InitializeComponent();
            App.AddWatermark(txtSearch, "search");
        }

        private void Speaker_Tap_1(object sender, GestureEventArgs e)
        {
            var speaker = (StackPanel)sender;
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