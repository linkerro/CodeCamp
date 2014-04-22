using System;
using System.ComponentModel;
using System.Windows;
using CodeCamp.Models;
using System.Net;
using Model;

namespace CodeCampWP7
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(Storage storage)
        {
            _storage=storage;
            _downloader = new Downloader(this,_storage);
        }

        private Event _event;
        private readonly Storage _storage;
        private readonly Downloader _downloader;

        public Event EventModel
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                NotifyPropertyChanged("EventModel");
            }
        }
        public Speaker SpeakerModel { get; set; }
        public Session SessionModel { get; set; }

      public bool IsDataLoaded
        {
            get;
            private set;
        }

        public bool IsUpdateLoaded { get; set; }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            TryGetEvent();
        }

        /// <summary>
        /// Tries to set the EventModel using data off local storage or off the internets.
        /// </summary>
        private void TryGetEvent()
        {
            IsUpdateLoaded = false;
            NotifyPropertyChanged("IsUpdateLoaded");
            _downloader.MakeRequest(OnFailure, OnSuccess);
        }

        public  void UpdateEvent(DownloadStringCompletedEventArgs eventArgs)
        {
            SetEvent(eventArgs.Result);
            IsUpdateLoaded = true;
            NotifyPropertyChanged("IsUpdateLoaded");
        }

        public void SetEvent(string eventJson)
        {
            EventModel = Event.Parse(eventJson);
            IsDataLoaded = true;
        }

        public void OnSuccess(DownloadStringCompletedEventArgs eventArgs)
        {
            UpdateEvent(eventArgs);
        }

        public void OnFailure()
        {
            MessageBox.Show("There was a problem contacting the server. Showing cached data.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}