using AusbildungsnachweisGenerator.Helper;
using AusbildungsnachweisGenerator.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AusbildungsnachweisGenerator.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var file = await IOHelper.SelectFile(new() { ".json" });
                if (file != null)
                {
                    var settings = JsonConvert.DeserializeObject<Settings>(file);
                    foreach (var profile in settings.Profiles)
                    {
                        AppHelper.AddProfile(profile);
                    }
                }
            }
            catch (Exception)
            {
                await new MessageDialog("Es ist ein Fehler beim importieren aufgetreten", "Fehler").ShowAsync();
            }
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var settings = AppHelper.GetSettings();
                if (settings != null)
                {
                    var file = await IOHelper.SelectFolder(new() { ".json" });
                    if (file != null)
                    {
                        File.WriteAllText($@"{file}\Ausbildungsnachweis_Generator_backup.json", JsonConvert.SerializeObject(settings));
                    }
                }
            }
            catch (Exception)
            {
                await new MessageDialog("Es ist ein Fehler beim exportieren aufgetreten", "Fehler").ShowAsync();
            }
        }
    }
}
