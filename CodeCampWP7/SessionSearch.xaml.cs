using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CodeCampWP7.ViewModels;
using CodeCamp.Models;
using System.Windows.Media;

namespace CodeCampWP7
{
    public partial class SessionSearch : PhoneApplicationPage
    {
        public SessionSearch()
        {
            DataContext = new SessionSearchViewModel() { Filter = string.Empty };

            InitializeComponent();

            App.AddWatermark(txtSearch, "search");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SessionSearchViewModel)DataContext).Filter = ((TextBox)sender).Text == "search" ? string.Empty : ((TextBox)sender).Text;
        }

        private void Session_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel session = (StackPanel)sender;
            App.ViewModel.SessionModel = (Session)session.DataContext;
            NavigationService.Navigate(new Uri("/SessionPage.xaml", UriKind.Relative));
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