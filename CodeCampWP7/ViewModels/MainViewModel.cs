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

namespace CodeCampWP7
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

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

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            #region sample data initialization
            this.Items.Add(new ItemViewModel() { LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime two", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime three", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime four", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime five", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime six", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime seven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime eight", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime nine", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime ten", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime eleven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime twelve", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime thirteen", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime fourteen", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime fifteen", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            this.Items.Add(new ItemViewModel() { LineOne = "runtime sixteen", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
            #endregion

            TryGetEvent();
        }

        /// <summary>
        /// Tries to set the EventModel using data off local storage or off the internets.
        /// </summary>
        private void TryGetEvent()
        {
            string eventJson=null;// = LoadFromStorage("lastEvent.js");
            if (!string.IsNullOrEmpty(eventJson))
            {
                EventModel = Event.Parse(eventJson);
                IsDataLoaded = true;
            }
            else
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(RequestCompleted);
                client.DownloadStringAsync(new Uri("http://tagonsoft.ro/codecamp.php"));
                //client.DownloadStringAsync(new Uri("http://testing.carflowmanager.be/AppPoolRecycler/Scripts/codecamp.js"));
                //client.DownloadStringAsync(new Uri("http://localhost/codecamp.js"));
            }
        }

        private void RequestCompleted(object sender, DownloadStringCompletedEventArgs eventArgs)
        {
            //TODO: stop the loading data animation when this is done.
            if (eventArgs.Error != null)
            {
                MessageBox.Show(eventArgs.Error.Message);
                return;
            }
            SaveToStorage("lastEvent.js", eventArgs.Result);
            EventModel = Event.Parse(eventArgs.Result);
            IsDataLoaded = true;
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