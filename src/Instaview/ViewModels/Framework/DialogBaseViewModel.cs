using CodedByCurtis.MVVM.ViewModel.Framework;
using Instaview.Services;

namespace Instaview.ViewModels.Framework
{
    /// <summary>
    /// Foundation for a dialog window's DataContext.
    /// </summary>
    public abstract class DialogBaseViewModel : ViewModelBase
    {
        /// <summary>
        /// The dialog window's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The message to display in the dialog window.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogBaseViewModel"/> class.
        /// </summary>
        public DialogBaseViewModel() : this("", "") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogBaseViewModel"/> class.
        /// </summary>
        /// <param name="title">The dialog window's title.</param>
        public DialogBaseViewModel(string title) : this(title, "") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogBaseViewModel"/> class.
        /// </summary>
        /// <param name="title">The dialog window's title.</param>
        /// <param name="message">The message to display in the dialog window.</param>
        public DialogBaseViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// Closes the specified <paramref name="window"/>.
        /// </summary>
        /// <param name="window">The dialog window to close.</param>
        public void CloseDialog(IDialogWindow window)
        {
            if (window is not null) { window.DialogResult = true; }
        }
    }
}
