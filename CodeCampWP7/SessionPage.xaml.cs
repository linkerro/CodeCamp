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
            StackPanel speaker = (StackPanel)sender;
            App.ViewModel.SpeakerModel = (Speaker)speaker.DataContext;
            NavigationService.Navigate(new Uri("/SpeakerPage.xaml", UriKind.Relative));
        }
    }
}