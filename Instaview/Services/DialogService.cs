using Instaview.ViewModels.Framework;
using Instaview.Views.Dialogs;

namespace Instaview.Services
{
    public class DialogService : IDialogService
    {
        public T OpenDialog<T>(DialogBaseViewModel<T> viewModel)
        {
            IDialogWindow window = new DialogWindow() { DataContext = viewModel };
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
