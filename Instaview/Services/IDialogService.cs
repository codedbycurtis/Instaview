using Instaview.ViewModels.Framework;

namespace Instaview.Services
{
    public interface IDialogService
    {
        T OpenDialog<T>(DialogBaseViewModel<T> viewModel);
    }
}
