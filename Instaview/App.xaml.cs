using System.IO;
using System.Windows;
using Instaview.Internal;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            // Check if a settings file exists, and if not, create one
            if (!File.Exists(Global.SettingsFilePath))
            {
                Global.Settings = new();
                Global.Settings.Save(Global.SettingsFilePath);
            }
            else { Global.Settings = Settings.Load(Global.SettingsFilePath); }

            base.OnStartup(e); // Perform default startup procedures
        }
    }
}
