using System.Windows.Input;
using CodedByCurtis.MVVM.ViewModel.Framework;
using Instaview.ViewModels.Framework;
using Instaview.Services;

namespace Instaview.ViewModels.Dialogs
{
    public class ExceptionDialogViewModel : DialogBaseViewModel
    {
        #region Commands

        /// <summary>
        /// Closes a <see cref="IDialogWindow"/>.
        /// </summary>
        public ICommand CloseCommand { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="ExceptionDialogViewModel"/> with the specified <paramref name="title"/> and <paramref name="message"/>.
        /// </summary>
        public ExceptionDialogViewModel(string title, string message) : base(title, message)
        {
            CloseCommand = new RelayCommand<IDialogWindow>((window) => { CloseDialog(window); });
        }

        #endregion
    }
}
