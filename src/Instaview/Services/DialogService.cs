using Instaview.ViewModels.Framework;
using Instaview.Views.Dialogs;

namespace Instaview.Services
{
    /// <summary>
    /// Provides a way of opening dialog windows.
    /// </summary>
    public class DialogService
    {
        /// <summary>
        /// Opens a new <see cref="DialogWindow"/> and assigns the specified <paramref name="viewModel"/> as its DataContext.
        /// </summary>
        /// <param name="viewModel">The DataContext of the <see cref="DialogWindow"/>.</param>
        public void OpenDialog(DialogBaseViewModel viewModel)
        {
            IDialogWindow window = new DialogWindow() { DataContext = viewModel };
            _ = window.ShowDialog();
        }
    }
}
