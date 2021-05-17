using Instaview.Services;

namespace Instaview.ViewModels.Framework
{
    public abstract class DialogBaseViewModel<T> : BaseViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public T DialogResult { get; set; }

        public DialogBaseViewModel() : this("", "") { }
        public DialogBaseViewModel(string title) : this(title, "") { }
        public DialogBaseViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog is not null) { dialog.DialogResult = true; }
        }
    }
}
