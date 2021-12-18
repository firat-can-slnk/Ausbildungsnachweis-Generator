using AusbildungsnachweisGenerator.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace AusbildungsnachweisGenerator
{
    public static class AppHelper
    {
        private static string settingsFilePath = "config.json";
        private static string GetSettingsFileContent()
        {
            return File.ReadAllText(settingsFilePath);
        }
        public static Settings GetSettings()
        {
            try
            {
                return JsonConvert.DeserializeObject<Settings>(GetSettingsFileContent());
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static void SaveSettings(Settings settings)
        {
            File.WriteAllText(settingsFilePath, JsonConvert.SerializeObject(settings));
        }
    }
}
