using System;
using System.Windows.Controls;
using CodeCamp.Models;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace CodeCampWP7
{
    public partial class SessionPage : PhoneApplicationPage
    {
        public SessionPage()
        {
            DataContext = App.ViewModel.SessionModel;
            InitializeComponent();
        }

        private void Speaker_Tap_1(object sender, GestureEventArgs e)
        {
            var speaker = (StackPanel)sender;
            App.ViewModel.SpeakerModel = (Speaker)speaker.DataContext;
            NavigationService.Navigate(new Uri("/SpeakerPage.xaml", UriKind.Relative));
        }
    }
}