using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using CodeCamp.Models;
using System.IO.IsolatedStorage;
using System.IO;
using System.Net;
using CodeCampWP7.Models;

namespace CodeCampWP7
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
        }

        private Event _event;
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
        public FullFeedback FeedbackModel { get; set; }

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
            string eventJson = LoadFromStorage("lastEvent.js");
            if (!string.IsNullOrEmpty(eventJson))
            {
                EventModel = Event.Parse(eventJson);
                IsDataLoaded = true;
            }
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(RequestCompleted);
            client.DownloadStringAsync(new Uri("http://tagonsoft.ro/codecamp.php"));
            IsUpdateLoaded = false;
            NotifyPropertyChanged("IsUpdateLoaded");
        }

        private void RequestCompleted(object sender, DownloadStringCompletedEventArgs eventArgs)
        {
            if (eventArgs.Error != null)
            {
                MessageBox.Show(eventArgs.Error.Message);
                return;
            }
            SaveToStorage("lastEvent.js", eventArgs.Result);
            EventModel = Event.Parse(eventArgs.Result);
            IsDataLoaded = true;
            IsUpdateLoaded = true;
            NotifyPropertyChanged("IsUpdateLoaded");
        }


        #region File interaction methods
        /// <summary>
        /// Save content to the given file name. If the file exists it is replaced by the new one, if it does not exist it is created.
        /// </summary>
        /// <param name="fileName">The name of the file to be created.</param>
        /// <param name="content">The string content to be saved to disk.</param>
        void SaveToStorage(string fileName, string content)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //if the file exists delete it, as we don't need the old version
                if (store.FileExists(fileName))
                    store.DeleteFile(fileName);

                using (IsolatedStorageFileStream file = store.OpenFile(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (var streamWriter = new StreamWriter(file))
                    {
                        streamWriter.Write(content);
                        streamWriter.Flush();
                    }
                }

            }

        }

        /// <summary>
        /// Load content form the given file.
        /// </summary>
        /// <param name="fileName">The name of the file that needs to be read.</param>
        /// <returns>The content of the file as a string.</returns>
        string LoadFromStorage(string fileName)
        {
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!store.FileExists(fileName))
                    return null;

                using (IsolatedStorageFileStream file = store.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(file))
                    {
                        return reader.ReadToEnd();
                    }
                }

            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}