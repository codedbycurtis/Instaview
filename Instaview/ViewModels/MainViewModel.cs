using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using Instasharp.Net;
using Instasharp.Profiles;
using Instaview.ViewModels.Base;
using Instaview.Models;
using Instaview.Internal;

namespace Instaview.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Members

        private readonly InstagramWebClient _instagramClient;

        #endregion

        #region Properties

        public string Title
        {
            get => $"Instaview {App.Version}";
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private Profile _profile;
        public Profile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; set; }
        public ICommand DownloadCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel()
        {
            _instagramClient = new(Global.Settings.SessionID);

            SearchCommand = new RelayCommand(async () => { Profile = await _instagramClient.GetProfileMetadataAsync(Username); });

            DownloadCommand = new RelayCommand(async () => { await _instagramClient.DownloadProfilePictureAsync(Profile, $"{Profile.Handle}.jpg"); });
        }

        #endregion

        #region Helper Methods

        private void ShowFileDialog()
        {
            SaveFileDialog dialog = new()
            {
                AddExtension = true,
                FileName = $"{Username}.jpg",
                Filter = "JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*",
                Title = "Download Profile Picture",
                ValidateNames = true,
            };

            dialog.ShowDialog();
        }

        #endregion
    }
}
