using Instaview.Services;

namespace Instaview.ViewModels.Framework
{
    public abstract class DialogBaseViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public DialogBaseViewModel() : this("", "") { }
        public DialogBaseViewModel(string title) : this(title, "") { }
        public DialogBaseViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public void CloseDialog(IDialogWindow window)
        {
            if (window is not null) { window.DialogResult = true; }
        }
    }
}
