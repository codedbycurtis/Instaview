using System.Windows.Input;
using Instaview.Services;
using Instaview.ViewModels.Framework;

namespace Instaview.ViewModels.Dialogs
{
    public class AboutDialogViewModel : DialogBaseViewModel<DialogResult>
    {
        #region Commands

        public ICommand CloseCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="AboutDialogViewModel"/> with the specified <paramref name="title"/>.
        /// </summary>
        public AboutDialogViewModel(string title) : base(title)
        {
            CloseCommand = new RelayCommand<IDialogWindow>((window) =>
            {
                CloseDialogWithResult(window, DialogResult.OK);
            });
        }

        #endregion
    }
}
