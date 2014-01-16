using CodeCamp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CodeCampWP7.ViewModels
{
    public class SpeakerSearchViewModel : INotifyPropertyChanged
    {
        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                FilteredSpeakers = App.ViewModel.EventModel.Speakers.Where(s => s.FullName.ToLower().Contains(_filter.ToLower())).ToList();
            }
        }
        private IEnumerable<Speaker> _filteredSpeakers;
        public IEnumerable<Speaker> FilteredSpeakers
        {
            get
            {
                return _filteredSpeakers;
            }
            set
            {
                _filteredSpeakers = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FilteredSpeakers"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
