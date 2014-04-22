using System;
using System.Windows.Controls;
using CodeCamp.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;
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

      private void AddReminder(object sender, GestureEventArgs e)
      {
        var reminder = new SaveAppointmentTask()
        {
          AppointmentStatus = AppointmentStatus.Tentative,
          Details = App.ViewModel.SessionModel.Description,
          EndTime = App.ViewModel.SessionModel.End,
          StartTime = App.ViewModel.SessionModel.Start,
          IsAllDayEvent = false,
          //don't ask why this cast is here, it just wouldn't build without it
          Location = App.ViewModel.SessionModel.Track != null ? (string) App.ViewModel.SessionModel.Track.Name : string.Empty,
          Reminder = Reminder.FiveMinutes,
          Subject = App.ViewModel.SessionModel.Title
        };
        reminder.Show();
      }
    }
}