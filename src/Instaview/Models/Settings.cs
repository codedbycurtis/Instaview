using System.IO;
using Newtonsoft.Json;

namespace Instaview.Models
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public partial struct Settings
    {
        /// <summary>
        /// The user's session identifier.
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionID { get; set; }

        /// <summary>
        /// Saves the current settings to the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The full path to the location in which you want to save the settings.</param>
        public void Save(string path)
        {
            var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            using FileStream fs = new(path, FileMode.Create);
            using StreamWriter sw = new(fs);
            sw.Write(jsonString);
        }
    }

    /// <inheritdoc />
    public partial struct Settings
    {
        /// <summary>
        /// Attempts to load settings from the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The full path to load the settings from.</param>
        /// <returns>The loaded settings.</returns>
        public static Settings Load(string path)
        {
            using FileStream fs = File.OpenRead(path);
            using StreamReader sr = new(fs);
            var jsonString = sr.ReadToEnd();
            try { return JsonConvert.DeserializeObject<Settings>(jsonString); }
            catch
            {
                fs.Close(); // Closes the afforementioned FileStream to prevent access exceptions
                return Settings.CreateNew(path);
            }
        }

        /// <summary>
        /// Creates a new settings file at the specified <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The full path to store the new settings file.</param>
        /// <returns>The new settings file.</returns>
        public static Settings CreateNew(string path)
        {
            var settings = new Settings();
            settings.Save(path);
            return settings;
        }
    }
}
