using System;
using System.Diagnostics;
using System.Windows.Input;
using Instaview.Services;
using Instaview.ViewModels.Framework;

namespace Instaview.ViewModels.Dialogs
{
    public class AboutDialogViewModel : DialogBaseViewModel
    {
        #region Commands

        /// <summary>
        /// Opens the <see cref="Uri"/> provided by a hyperlink.
        /// </summary>
        public ICommand OpenHyperlinkCommand { get; }

        /// <summary>
        /// Closes a <see cref="IDialogWindow"/>.
        /// </summary>
        public ICommand CloseCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="AboutDialogViewModel"/> with the specified <paramref name="title"/>.
        /// </summary>
        public AboutDialogViewModel(string title) : base(title)
        {
            OpenHyperlinkCommand = new RelayCommand<Uri>((uri) =>
            {
                _ = Process.Start(new ProcessStartInfo(uri.ToString()) { UseShellExecute = true });
            });

            CloseCommand = new RelayCommand<IDialogWindow>((window) =>
            {
                CloseDialog(window);
            });
        }

        #endregion
    }
}
