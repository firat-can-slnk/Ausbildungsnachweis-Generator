using AusbildungsnachweisGenerator.Model;
using AusbildungsnachweisGenerator.ViewModel;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AusbildungsnachweisGenerator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            DataContext = new SettingsPageViewModel();
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 960)
            {
                Grid.SetColumn(GridRight, 0);
                Grid.SetRow(GridRight, 1);
            }
            else
            {
                Grid.SetColumn(GridRight, 1);
                Grid.SetRow(GridRight, 0);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("");

            var dataContext = (SettingsPageViewModel)DataContext;

            var objectToSave = new Settings(dataContext.Job, dataContext.Instructor, dataContext.Company, dataContext.Apprenticeship, dataContext.Apprentice, dataContext.Address);

            AppHelper.SaveSettings(objectToSave);
            
        }
    }
}
