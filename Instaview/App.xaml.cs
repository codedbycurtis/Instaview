using System.IO;
using System.Windows;
using System.Reflection;
using Instaview.Models;
using Instaview.ViewModels.Dialogs;
using Instaview.Utils;

namespace Instaview
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The string representation of this application's current major, minor, and build versions.
        /// </summary>
        public static string Version { get; } = typeof(App).Assembly.GetName().Version.ToString(3);

        /// <summary>
        /// The string representation of the Instasharp API's current major, minor, and build versions.
        /// </summary>
        public static string ApiVersion { get; } = AssemblyName.GetAssemblyName("Instasharp.dll").Version.ToString(3);

        /// <summary>
        /// The title of this application.
        /// </summary>
        public static string Title { get; } = $"Instaview {Version}";

        /// <summary>
        /// The normalized path to the application's settings file.
        /// </summary>
        public static string SettingsFilePath { get; } = "settings.json";

        /// <summary>
        /// The application's global <see cref="Settings"/>.
        /// </summary>
#pragma warning disable CA2211
        public static Settings Settings;
#pragma warning restore CA2211

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ensures that a DialogWindow is created for all types of exceptions
            Application.Current.DispatcherUnhandledException += (_, e) =>
            {
                Dialog.Service.OpenDialog(new ExceptionDialogViewModel("Something went wrong", e.Exception.Message));
                e.Handled = true;
            };

            // Check if a settings file exists, and if not, create one
            Settings = File.Exists(SettingsFilePath) ? Settings.Load(SettingsFilePath) : Settings.CreateNew(SettingsFilePath);

            base.OnStartup(e); // Perform default startup procedures
        }
    }
}
