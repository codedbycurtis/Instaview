using System.Windows.Input;
using System.Collections.Generic;
using static System.Environment;
using Microsoft.Win32;
using Instasharp;
using Instasharp.Profiles;
using Instasharp.Exceptions;
using Instasharp.Search;
using Instaview.ViewModels.Framework;
using Instaview.Services;
using Instaview.ViewModels.Dialogs;

namespace Instaview.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Members

        private readonly InstagramClient _instagramClient;
        private readonly IDialogService _dialogService;

        #endregion

        #region Properties

        private bool _isProfileOnDisplay;
        public bool IsProfileOnDisplay
        {
            get => _isProfileOnDisplay;
            set => SetProperty(ref _isProfileOnDisplay, value);
        }

        private bool _profileHasWebsite;
        public bool ProfileHasWebsite
        {
            get => _profileHasWebsite;
            set => SetProperty(ref _profileHasWebsite, value);
        }

        private bool _profileHasBio;
        public bool ProfileHasBio
        {
            get => _profileHasBio;
            set => SetProperty(ref _profileHasBio, value);
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

        public ICommand SearchCommand { get; }
        public ICommand DownloadCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="MainViewModel"/>.
        /// </summary>
        public MainViewModel()
        {
            _instagramClient = new(App.Settings.SessionID);
            _dialogService = new DialogService();


            SearchCommand = new RelayCommand(async () =>
            {
                if (!string.IsNullOrEmpty(UsernameOrUrl))
                {
                    try { Profile = await _instagramClient.GetProfileMetadataAsync(UsernameOrUrl); }
                    catch (InstasharpException ex) { _dialogService.OpenDialog(new ExceptionDialogViewModel("Something went wrong", ex.Message)); }
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
                _dialogService.OpenDialog(new AboutDialogViewModel("About the App"));
            });

            ShowSettingsCommand = new RelayCommand(() =>
            {
                _dialogService.OpenDialog(new SettingsDialogViewModel("Settings"));
            });
        }

        #endregion
    }
}
