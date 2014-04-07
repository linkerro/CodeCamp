using System;
using System.Windows.Controls;
using Model;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace CodeCampWP7
{
    public partial class SpeakerPage
    {
        public SpeakerPage()
        {
            DataContext = App.ViewModel.SpeakerModel;
            InitializeComponent();
        }

        private void Session_Tap_1(object sender, GestureEventArgs e)
        {
            var session = (StackPanel)sender;
            App.ViewModel.SessionModel = (Session)session.DataContext;
            NavigationService.Navigate(new Uri("/SessionPage.xaml", UriKind.Relative));
        }
    }
}