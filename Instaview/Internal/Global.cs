using Instaview.Models;

namespace Instaview.Internal
{
    /// <summary>
    /// Application-scope-accessible methods and properties.
    /// </summary>
    public class Global
    {
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
    }
}
