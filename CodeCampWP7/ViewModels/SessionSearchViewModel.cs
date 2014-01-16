using CodeCamp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CodeCampWP7.ViewModels
{
    public class SessionSearchViewModel : INotifyPropertyChanged
    {
        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                FilteredSessions = (from s in App.ViewModel.EventModel.Sessions
                                    where (s.Description + " "
                                        + s.Title + " "
                                        + (s.Track == null ? "" : s.Track.Name) + " "
                                        + (s.SpeakerList.Count > 0 ? s.SpeakerList.Select(x => x.FullName).Aggregate((a, n) => a + " " + n) : string.Empty))
                                        .ToLower().Contains(_filter.ToLower())
                                    orderby s.Start
                                    select s).ToArray();

            }
        }
        private IEnumerable<Session> _filteredSessions;
        public IEnumerable<Session> FilteredSessions
        {
            get
            {
                return _filteredSessions;
            }
            set
            {
                _filteredSessions = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredSessions"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
