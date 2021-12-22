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
using AusbildungsnachweisGenerator.Helper;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.Toolkit.Mvvm.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AusbildungsnachweisGenerator.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        private double progress = double.NaN;
        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                if (!double.IsNaN(progress))
                {
                    GenerateButton.IsEnabled = false;
                    if (progress > 2)
                    {
                        ProgressRingMain.IsActive = true;
                        ProgressRingMain.IsIndeterminate = false;
                        ProgressRingMain.Value = progress;
                    }
                    else
                    {
                        ProgressRingMain.IsActive = true;
                        ProgressRingMain.IsIndeterminate = true;
                        ProgressRingMain.Value = 0;
                    }
                }
                else
                {
                    ProgressRingMain.IsActive = false;
                    ProgressRingMain.IsIndeterminate = true;
                    ProgressRingMain.Value = 0;
                    SetGenerateButtonIsEnabledBinding();
                }
            }
        }

        public StartPage()
        {
            InitializeComponent();
            DataContext = new StartPageViewModel();
            SetGenerateButtonIsEnabledBinding();
        }
        private void SetGenerateButtonIsEnabledBinding()
        {
            var binding = new Binding();
            binding.Path = new PropertyPath("IsFormValid");
            binding.Mode = BindingMode.OneWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(GenerateButton, Button.IsEnabledProperty, binding);
        }
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Progress = 1;
            var dt = (StartPageViewModel)DataContext;
            new Task(() => GenerateProofs(dt)).Start();
        }

        private void GenerateProofs(StartPageViewModel vm)
        {
            try
            {
                var dataContext = vm;

                var generated = 0;

                var profile = dataContext.SelectedProfile;
                var noteNr = 0;

                var startDate = dataContext.StartDate.DateTime;
                var endDate = dataContext.EndDate.DateTime;

                var dates = new ProofDates(startDate, endDate, dataContext.FilePath);

                var max = dates.Weeks.Count();

                var type = dataContext.SelectedProofType();

                foreach (var year in dates.Years)
                {
                    var yearPath = @$"{dates.RootPath}\{dates.YearDirectory(year)}";
                    Directory.CreateDirectory(yearPath);

                    noteNr++;

                    foreach (var month in dates.MonthsInYear(year))
                    {
                        if (month.Year == year.Year)
                        {
                            var monthPath = @$"{yearPath}\{dates.MonthDirectory(month)}";
                            Directory.CreateDirectory(monthPath);


                            foreach (var week in dates.WeeksInMonth(month))
                            {
                                var proof = profile.GetProof(noteNr, week.StartOfWeek(), week.EndOfWeek());
                                _ = proof.GenerateDocument(monthPath, type);
                                generated++;

                                DispatcherQueue.TryEnqueue(() =>
                                {
                                    Progress = (generated / (double)max) * 100;
                                });
                            }

                        }
                    }
                }

                DispatcherQueue.TryEnqueue(async () =>
                {
                    var dialog = new ContentDialog()
                    {
                        Content = "Die Berichte wurden erfolgreich erstellt.",
                        Title = "Erfolgreich erstellt",
                        CloseButtonText = "Schließen",
                        PrimaryButtonText = "Speicherort öffnen"
                    };
                    dialog.XamlRoot = Content.XamlRoot;
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                        IOHelper.OpenWithDefaultProgram(dates.RootPath);
                });
            }
            catch (Exception)
            {
                DispatcherQueue.TryEnqueue(async () =>
                {
                    var dialog = new MessageDialog("Es ist ein Fehler beim erstellen der Ausbildungsnachweise aufgetreten", "Fehler");

                    AppHelper.InitializeWithWindow(dialog);

                    await dialog.ShowAsync();
                });
            }
            DispatcherQueue.TryEnqueue(() =>
            {
                Progress = double.NaN;
            });
        }

        private async void FilePathButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (StartPageViewModel)DataContext;

            var path = await IOHelper.SelectFolder();
            if (path != null)
                dataContext.FilePath = path;
        }

        private void SampleButton_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = (StartPageViewModel)DataContext;

            var proof = dataContext.SelectedProfile == null || dataContext.SelectedProfile.IsForCreation ? Proof.Sample : dataContext.SelectedProfile.GetProof(1, DateTime.Now.StartOfWeek(), DateTime.Now.EndOfWeek());

            var path = @$"{Path.GetTempPath()}";
            path = path.Remove(path.Count() - 1);

            var type = dataContext.SelectedProofType();

            var filePath = proof.GenerateDocument(@$"{path}", type);

            if (File.Exists(filePath))
                IOHelper.OpenWithDefaultProgram(filePath);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e) => ((StartPageViewModel)DataContext).LoadProfiles();

        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (DataContext is StartPageViewModel dc)
                dc.UpdateDates();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs args) => HelpTeachingTipProfileHeader.IsOpen = true;
        private void ShowNextTeachingTip(TeachingTip sender, object args)
        {
            sender.IsOpen = false;

            if (sender == HelpTeachingTipProfileHeader)
            {
                HelpTeachingTipTimeFrameHeader.IsOpen = true;
            }
            else if (sender == HelpTeachingTipTimeFrameHeader)
            {
                HelpTeachingTipTypeHeader.IsOpen = true;
            }
            else if (sender == HelpTeachingTipTypeHeader)
            {
                HelpTeachingTipSaveDirectoryHeader.IsOpen = true;
            }
            else if (sender == HelpTeachingTipSaveDirectoryHeader)
            {
                HelpTeachingTipGenerateButton.IsOpen = true;
            }
            else if (sender == HelpTeachingTipGenerateButton)
            {
                HelpTeachingTipSampleButton.IsOpen = true;
            }
            else if (sender == HelpTeachingTipSampleButton)
            {
                HelpTeachingTipProofTreeViewSection.IsOpen = true;
            }
        }
    }
}
