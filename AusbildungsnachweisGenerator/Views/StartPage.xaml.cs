using AusbildungsnachweisGenerator.Extensions;
using AusbildungsnachweisGenerator.Model;
using AusbildungsnachweisGenerator.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using AusbildungsnachweisGenerator;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AusbildungsnachweisGenerator.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            this.InitializeComponent();
            DataContext = new StartPageViewModel();
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (StartPageViewModel)DataContext;

            var settings = AppHelper.GetSettings();
            var noteNr = 0;

            var startDate = dataContext.StartDate.DateTime.StartOfWeek();
            var endDate = dataContext.EndDate.DateTime.EndOfWeek();

            var years = startDate.GetYearlyDateRangeTo(endDate);
            foreach (var year in years)
            {
                var yearPath = @$"{dataContext.FilePath}\{year.Year}";
                Directory.CreateDirectory(yearPath);

                noteNr++;

                var months = startDate.GetMonthlyDateRangeTo(endDate);
                foreach (var month in months)
                {
                    if(month.Year == year.Year)
                    {
                        var monthPath = @$"{yearPath}\{month.Month} {month.ToString("MMMM")}";
                        Directory.CreateDirectory(monthPath);

                        var weeks = startDate.GetWeeklyDateRangeTo(endDate);

                        foreach (var week in weeks)
                        {
                            if (week.Month == month.Month && week.Year == month.Year)
                            {
                                var proof = new Proof(noteNr.ToString(), 
                                    settings.Apprenticeship, 
                                    settings.Apprentice, 
                                    settings.Address, 
                                    settings.Instructor, 
                                    settings.Job, 
                                    settings.Company, 
                                    noteNr, 
                                    week.StartOfWeek(), 
                                    week.EndOfWeek(), 
                                    hourRate: settings.Apprenticeship.HourRate);
                                proof.GenerateDocument(monthPath);
                            }
                        }

                    }
                }
            }

            
        }

        private async void FilePathButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (StartPageViewModel)DataContext;

            // Need to get the hwnd (“window” is a pointer to a WinUI Window object). 
            // WinRT.Interop namespace is provided by C#/WinRT projected interop wrappers for .NET 5+
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Window);

            var picker = new FolderPicker();

            // Need to initialize the picker object with the hwnd / IInitializeWithWindow 
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            // Now we can use the picker object as normal
            picker.FileTypeFilter.Add("*");
            var folder = await picker.PickSingleFolderAsync();
            dataContext.FilePath = folder?.Path;
        }
    }
}
