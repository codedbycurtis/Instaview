using System.Windows.Input;
using static System.Environment;
using Microsoft.Win32;
using Instasharp;
using Instasharp.Profiles;
using Instaview.ViewModels.Framework;
using Instaview.ViewModels.Dialogs;
using Instaview.Utils;

namespace Instaview.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Members

        private readonly InstagramClient _instagramClient;

        #endregion

        #region Properties

        private bool _isProfileOnDisplay;
        public bool IsProfileOnDisplay
        {
            get => _isProfileOnDisplay;
            set => SetProperty(ref _isProfileOnDisplay, value);
        }

        private bool _profileHasFullName;
        public bool ProfileHasFullName
        {
            get => _profileHasFullName;
            set => SetProperty(ref _profileHasFullName, value);
        }

        private bool _profileHasBusinessCategorySpecified;
        public bool ProfileHasBusinessCategorySpecified
        {
            get => _profileHasBusinessCategorySpecified;
            set => SetProperty(ref _profileHasBusinessCategorySpecified, value);
        }

        private bool _profileHasBio;
        public bool ProfileHasBio
        {
            get => _profileHasBio;
            set => SetProperty(ref _profileHasBio, value);
        }

        private bool _profileHasWebsite;
        public bool ProfileHasWebsite
        {
            get => _profileHasWebsite;
            set => SetProperty(ref _profileHasWebsite, value);
        }

        private string _usernameOrUrl;
        public string UsernameOrUrl
        {
            get => _usernameOrUrl;
            set => SetProperty(ref _usernameOrUrl, value);
        }

        private Profile _profile;
        public Profile Profile
        {
            get => _profile;
            set
            {
                SetProperty(ref _profile, value);
                IsProfileOnDisplay = true;
                ProfileHasFullName = !string.IsNullOrEmpty(Profile.FullName);
                ProfileHasBusinessCategorySpecified = !string.IsNullOrEmpty(Profile.BusinessCategoryName);
                ProfileHasBio = !string.IsNullOrEmpty(Profile.Bio);
                ProfileHasWebsite = !string.IsNullOrEmpty(Profile.Website);
            }
        }

        //private IReadOnlyList<ProfileSearchResult> _requestedProfiles;
        //public IReadOnlyList<ProfileSearchResult> RequestedProfiles
        //{
        //    get => _requestedProfiles;
        //    set => SetProperty(ref _requestedProfiles, value);
        //}

        #endregion

        #region Commands

        /// <summary>
        /// Searches for the specified <see cref="UsernameOrUrl"/>.
        /// </summary>
        public ICommand SearchCommand { get; }

        /// <summary>
        /// Downloads an Instagram account's profile picture.
        /// </summary>
        public ICommand DownloadCommand { get; }

        /// <summary>
        /// Opens a new dialog window for the <see cref="AboutDialogViewModel"/>.
        /// </summary>
        public ICommand ShowAboutCommand { get; }

        /// <summary>
        /// Opens a new dialog window for the <see cref="SettingsDialogViewModel"/>.
        /// </summary>
        public ICommand ShowSettingsCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel()
        {
            _instagramClient = new(App.Settings.SessionID);

            SearchCommand = new RelayCommand(async () =>
            {
                if (!string.IsNullOrEmpty(UsernameOrUrl))
                {
                    Profile = await _instagramClient.GetProfileMetadataAsync(UsernameOrUrl);
                }
            });

            DownloadCommand = new RelayCommand(async () =>
            {
                SaveFileDialog saveFileDialog = new()
                {
                    AddExtension = true,
                    FileName = $"{Profile.Handle}.jpg",
                    Filter = "JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*",
                    InitialDirectory = GetFolderPath(SpecialFolder.DesktopDirectory),
                    Title = "Download Profile Picture",
                    ValidateNames = true,
                };

                if (saveFileDialog.ShowDialog() is true)
                {
                    var path = saveFileDialog.FileName;
                    await _instagramClient.DownloadProfilePictureAsync(Profile, path);
                }
            });

            ShowAboutCommand = new RelayCommand(() =>
            {
                Dialog.Service.OpenDialog(new AboutDialogViewModel("About"));
            });

            ShowSettingsCommand = new RelayCommand(() =>
            {
                Dialog.Service.OpenDialog(new SettingsDialogViewModel("Settings"));
            });
        }

        #endregion
    }
}
