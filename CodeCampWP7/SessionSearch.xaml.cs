using System;
using System.Windows.Controls;
using CodeCampWP7.ViewModels;
using Model;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace CodeCampWP7
{
    public partial class SessionSearch
    {
        public SessionSearch()
        {
            DataContext = new SessionSearchViewModel { Filter = string.Empty };

            InitializeComponent();

            App.AddWatermark(txtSearch, "search");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((SessionSearchViewModel)DataContext).Filter = ((TextBox)sender).Text == "search" ? string.Empty : ((TextBox)sender).Text;
        }

        private void Session_Tap_1(object sender, GestureEventArgs e)
        {
            var session = (StackPanel)sender;
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