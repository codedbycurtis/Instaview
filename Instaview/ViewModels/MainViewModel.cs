using System.Windows.Input;
using System.Collections.Generic;
using static System.Environment;
using Microsoft.Win32;
using Instasharp;
using Instasharp.Profiles;
using Instasharp.Exceptions;
using Instaview.ViewModels.Base;
using Instaview.Internal;
using Instasharp.Search;

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
                ProfileHasWebsite = !string.IsNullOrEmpty(Profile.Website);
            }
        }

        private IReadOnlyList<ProfileSearchResult> _requestedProfiles;
        public IReadOnlyList<ProfileSearchResult> RequestedProfiles
        {
            get => _requestedProfiles;
            set => SetProperty(ref _requestedProfiles, value);
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

            SearchCommand = new RelayCommand(async () =>
            {
                if (!string.IsNullOrEmpty(UsernameOrUrl))
                {
                    try { Profile = await _instagramClient.GetProfileMetadataAsync(UsernameOrUrl); }
                    catch (ProfileNotFoundException) { } // TODO: Implement dialog for exceptions
                }
            });

            DownloadCommand = new RelayCommand(async () =>
            {
                SaveFileDialog dialog = new()
                {
                    AddExtension = true,
                    FileName = $"{Profile.Handle}.jpg",
                    Filter = "JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*",
                    InitialDirectory = GetFolderPath(SpecialFolder.DesktopDirectory),
                    Title = "Download Profile Picture",
                    ValidateNames = true,
                };

                if (dialog.ShowDialog() is true)
                {
                    var path = dialog.FileName;
                    await _instagramClient.DownloadProfilePictureAsync(Profile, path);
                }
            });
        }

        #endregion
    }
}
