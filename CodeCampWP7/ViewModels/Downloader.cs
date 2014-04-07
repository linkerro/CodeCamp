using System;
using System.Net;

namespace CodeCampWP7
{
    public class Downloader
    {
        private readonly MainViewModel _mainViewModel;
        private readonly Storage _storage;

        public Downloader(MainViewModel mainViewModel, Storage storage)
        {
            _mainViewModel = mainViewModel;
            _storage = storage;
        }

        public void MakeRequest(Action onFailure, Action<DownloadStringCompletedEventArgs> onSuccess)
        {
            var eventJson = _storage.LoadFromStorage("lastEvent.js");
            if (!string.IsNullOrEmpty(eventJson))
            {
                _mainViewModel.SetEvent(eventJson);
            }
            var client = new WebClient();
            client.DownloadStringCompleted +=
                (sender, eventArgs) => RequestCompleted(sender, eventArgs, onFailure, onSuccess);
            client.Headers["X-ZUMO-APPLICATION"] = "rvFDWxpvbJyobzmGuNFmUghdIxSqwQ70";
            client.DownloadStringAsync(new Uri("https://codecampevents.azure-mobile.net/tables/event"));
        }

        private void RequestCompleted(object sender, DownloadStringCompletedEventArgs eventArgs, Action onFailure, Action<DownloadStringCompletedEventArgs> onSuccess)
        {
            if (eventArgs.Error != null)
            {
                onFailure();
                return;
            }
            _storage.SaveToStorage("lastEvent.js", eventArgs.Result);
            onSuccess(eventArgs);
        }
    }
}