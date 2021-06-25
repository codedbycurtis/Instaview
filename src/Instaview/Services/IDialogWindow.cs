namespace Instaview.Services
{
    /// <summary>
    /// Differentiates a dialog window from a regular window.
    /// </summary>
    public interface IDialogWindow
    {
        bool? DialogResult { get; set; }
        object DataContext { get; set; }
        bool? ShowDialog();
    }
}
