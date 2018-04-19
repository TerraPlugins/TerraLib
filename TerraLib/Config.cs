using Newtonsoft.Json;
using System.IO;
using TShockAPI;

namespace TerraLib
{
    public static class Config
    {
        /// <summary>
        /// First tries to read from the file, if it doesn't exist it will create the file.
        /// </summary>
        /// <typeparam name="T">The class to deserielize the configuration to.</typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T Read<T>(string fileName) where T : new()
        {
            string path = GetPath(fileName);

            if (!File.Exists(path))
            {
                T config = new T();

                File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
                return config;
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <param name="obj">The object to be serialized and saved.</param>
        /// <param name="fileName"></param>
        public static void Save(object obj, string fileName)
        {
            string path = GetPath(fileName);
            File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented));
        }

        private static string GetPath(string fileName)
        {
            var jsonFileName = Path.ChangeExtension(fileName, ".json");
            return Path.Combine(TShock.SavePath, jsonFileName);
        }
    }
}
