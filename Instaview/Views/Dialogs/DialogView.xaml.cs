using System.Windows;
using Instaview.Services;

namespace Instaview.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow() => InitializeComponent();
    }
}
