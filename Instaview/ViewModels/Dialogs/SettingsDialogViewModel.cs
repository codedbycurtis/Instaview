using System.Windows.Input;
using Instaview.ViewModels.Framework;
using Instaview.Services;

namespace Instaview.ViewModels.Dialogs
{
    public class SettingsDialogViewModel : DialogBaseViewModel
    {
        #region Properties

        private string _sessionId;
        public string SessionId
        {
            get => _sessionId;
            set => SetProperty(ref _sessionId, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Closes a <see cref="IDialogWindow"/>.
        /// </summary>
        public ICommand CloseCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="SettingsDialogViewModel"/> and the specified <paramref name="title"/>.
        /// </summary>
        public SettingsDialogViewModel(string title) : base(title)
        {
            SessionId = App.Settings.SessionID;

            CloseCommand = new RelayCommand<IDialogWindow>((window) =>
            {
                if (SessionId != App.Settings.SessionID)
                {
                    App.Settings.SessionID = SessionId;
                    App.Settings.Save(App.SettingsFilePath);
                }
                CloseDialog(window);
            });
        }

        #endregion
    }
}
