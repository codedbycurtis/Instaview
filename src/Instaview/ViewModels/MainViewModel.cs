using System.Windows.Input;
using static System.Environment;
using Microsoft.Win32;
using CodedByCurtis.MVVM.ViewModel.Framework;
using Instasharp;
using Instasharp.Profiles;
using Instaview.ViewModels.Dialogs;
using Instaview.Utils;

namespace Instaview.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private bool _isProfileOnDisplay;
        private bool _profileHasFullName;
        private bool _profileHasBusinessCategorySpecified;
        private bool _profileHasBio;
        private bool _profileHasWebsite;
        private bool _extendedInformationAvailable;
        private string _usernameOrUrl;
        private Profile _profile;
        private readonly InstagramClient _instagramClient;

        #endregion

        #region Properties

        /// <summary>
        /// Is the profile panel visible?
        /// </summary>
        public bool IsProfileOnDisplay
        {
            get => _isProfileOnDisplay;
            set => SetProperty(ref _isProfileOnDisplay, value);
        }

        /// <summary>
        /// Does the profile have a FullName property?
        /// </summary>
        public bool ProfileHasFullName
        {
            get => _profileHasFullName;
            set => SetProperty(ref _profileHasFullName, value);
        }

        /// <summary>
        /// Is the profile's Business Category specified?
        /// </summary>
        public bool ProfileHasBusinessCategorySpecified
        {
            get => _profileHasBusinessCategorySpecified;
            set => SetProperty(ref _profileHasBusinessCategorySpecified, value);
        }

        /// <summary>
        /// Does the profile have a bio?
        /// </summary>
        public bool ProfileHasBio
        {
            get => _profileHasBio;
            set => SetProperty(ref _profileHasBio, value);
        }

        /// <summary>
        /// Does the profile have a linked website?
        /// </summary>
        public bool ProfileHasWebsite
        {
            get => _profileHasWebsite;
            set => SetProperty(ref _profileHasWebsite, value);
        }

        /// <summary>
        /// Is the extended information panel visible?
        /// </summary>
        public bool ExtendedInformationAvailable
        {
            get => _extendedInformationAvailable;
            set => SetProperty(ref _extendedInformationAvailable, value);
        }

        /// <summary>
        /// The user-specified search query.
        /// </summary>
        public string UsernameOrUrl
        {
            get => _usernameOrUrl;
            set => SetProperty(ref _usernameOrUrl, value);
        }

        /// <summary>
        /// The profile searched for by the user.
        /// </summary>
        public Profile Profile
        {
            get => _profile;
            set
            {
                SetProperty(ref _profile, value);
                IsProfileOnDisplay = true;
                ProfileHasFullName = !string.IsNullOrEmpty(Profile.FullName);
                ProfileHasBio = !string.IsNullOrEmpty(Profile.Bio);
                ProfileHasWebsite = !string.IsNullOrEmpty(Profile.Website);
                ProfileHasBusinessCategorySpecified = !string.IsNullOrEmpty(Profile.BusinessCategoryName);
                if (ProfileHasBio || ProfileHasWebsite)
                {
                    ExtendedInformationAvailable = true;
                    return;
                }
                ExtendedInformationAvailable = false;
            }
        }

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
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
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
