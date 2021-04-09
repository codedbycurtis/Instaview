using System.IO;
using Newtonsoft.Json;

namespace Instaview.Models
{
    public partial class Settings
    {
        [JsonProperty("sessionId")]
        public string SessionID { get; set; }

        public void Save(string path)
        {
            var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            using FileStream fs = new(path, FileMode.Create);
            using StreamWriter sw = new(fs);
            sw.Write(jsonString);
        }        
    }

    public partial class Settings
    {
        public static Settings Load(string path)
        {
            using FileStream fs = File.OpenRead(path);
            using StreamReader sr = new(fs);
            var jsonString = sr.ReadToEnd();
            return JsonConvert.DeserializeObject<Settings>(jsonString);
        }
    }
}
