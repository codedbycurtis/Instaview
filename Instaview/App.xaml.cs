using System.IO;
using System.Windows;
using Instaview.Models;

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
        /// The title of this application.
        /// </summary>
        public static string Title { get; } = $"Instaview {Version}";

        /// <summary>
        /// The normalized path to the application's settings file.
        /// </summary>
        public static string SettingsFilePath { get; } = "settings.json";

        /// <summary>
        /// The application's global <see cref="Models.Settings"/>.
        /// </summary>
#pragma warning disable CA2211
        public static Settings Settings;
#pragma warning restore CA2211

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            // Check if a settings file exists, and if not, create one
            if (!File.Exists(SettingsFilePath))
            {
                Settings = new();
                Settings.Save(SettingsFilePath);
            }
            else { Settings = Settings.Load(SettingsFilePath); }

            base.OnStartup(e); // Perform default startup procedures
        }
    }
}
