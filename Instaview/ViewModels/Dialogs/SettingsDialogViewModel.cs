using System.Windows.Input;
using Instaview.ViewModels.Framework;
using Instaview.Services;

namespace Instaview.ViewModels.Dialogs
{
    public class SettingsDialogViewModel : DialogBaseViewModel<DialogResult>
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

        public ICommand CloseCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="SettingsDialogViewModel"/> with the specified <paramref name="title"/>.
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
                CloseDialogWithResult(window, DialogResult.OK);
            });
        }

        #endregion
    }
}
