using AusbildungsnachweisGenerator.Extensions;
using AusbildungsnachweisGenerator.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.ViewModel
{
    public class StartPageViewModel : ObservableObject
    {
        private DateTimeOffset startDate = DateTimeOffset.Now;
        private DateTimeOffset endDate = DateTimeOffset.Now.AddDays(14);
        private string filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\Ausbildungsnachweis";
        private Profile selectedProfile;
        private List<Profile> profiles;

        public DateTimeOffset StartDate
        {
            get => startDate;
            set
            {
                SetProperty(ref startDate, value);
                OnPropertyChanged(nameof(IsFormValid));
                OnPropertyChanged(nameof(HelpText));
                OnPropertyChanged(nameof(ProofTreeViewMain));
            }
        }
        public DateTimeOffset EndDate
        {
            get => endDate;
            set
            {
                SetProperty(ref endDate, value);
                OnPropertyChanged(nameof(IsFormValid));
                OnPropertyChanged(nameof(HelpText));
                OnPropertyChanged(nameof(ProofTreeViewMain));
            }
        }

        public string FilePath
        {
            get => filePath;
            set
            {
                SetProperty(ref filePath, value);
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        public List<Profile> Profiles
        {
            get => profiles;
            set
            {
                SetProperty(ref profiles, value);
                OnPropertyChanged(nameof(IsFormValid));
            }
        }

        public Profile SelectedProfile
        {
            get => selectedProfile;
            set
            {
                SetProperty(ref selectedProfile, value);
                OnPropertyChanged(nameof(IsFormValid));
                if (selectedProfile != null && !selectedProfile.IsForCreation)
                {
                    if (selectedProfile.Apprenticeship?.StartDate is DateTimeOffset start)
                    {
                        StartDate = start;
                    }
                    if (selectedProfile.Apprenticeship?.EndDate is DateTimeOffset end)
                    {
                        EndDate = end;
                    }
                }
            }
        }

        public bool IsFormValid => StartDate < EndDate &&
            FilePath != null &&
            !FilePath.Any(x => Path.GetInvalidPathChars().Contains(x)) &&
            SelectedProfile != null;
        public string HelpText => GetHelpText();

        private string GetHelpText()
        {
            var start = StartDate.DateTime;
            var end = EndDate.DateTime;

            var dates = new ProofDates(start, end, "");

            var weeksCount = dates.Weeks.Count();
            var monthsCount = dates.Months.Count();
            var yearCount = dates.Years.Count();
            var proofCount = weeksCount;

            var sb = new StringBuilder();

            if (proofCount < 1)
                return "";

            sb.Append($"Es {(proofCount != 1 ? "werden" : "wird")} insgesamt {proofCount} Berichte erstellt, ");
            sb.Append($"aufgeteilt in {weeksCount} Woche{(weeksCount != 1 ? "n" : "")}, ");
            sb.Append($"{monthsCount} Monat{(monthsCount != 1 ? "e" : "")} und ");
            sb.Append($"{yearCount} Jahr{(yearCount != 1 ? "e" : "")}");

            return sb.ToString();
        }

        public ObservableCollection<ProofTreeView> ProofTreeViewMain => GetProofTreeView();

        private ObservableCollection<ProofTreeView> GetProofTreeView()
        {
            var tv = ProofExtensions.GetTreeViewNode(StartDate.DateTime, EndDate.DateTime);
            return tv;
        }

        public bool TypeIsDaily { get; set; } = true;
        public bool TypeIsWeekly { get; set; } = false;
        public bool TypeIsPlan { get; set; } = false;

        public ProofType SelectedProofType() => Proof.GetProofTypeFromOptions(TypeIsDaily, TypeIsWeekly, TypeIsPlan);

        public void LoadProfiles() => Profiles = AppHelper.GetSettings().Profiles;

        internal void UpdateDates()
        {
            OnPropertyChanged(nameof(StartDate));
            OnPropertyChanged(nameof(EndDate));
            OnPropertyChanged(nameof(IsFormValid));
            OnPropertyChanged(nameof(HelpText));
            OnPropertyChanged(nameof(ProofTreeViewMain));
        }
    }
    public class ProofTreeViewTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FolderTemplate { get; set; }
        public DataTemplate FileTemplate { get; set; }
        public DataTemplate RootTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            var explorerItem = (ProofTreeView)item;
            switch (explorerItem.Type)
            {
                case ProofTreeViewType.File:
                    return FileTemplate;
                case ProofTreeViewType.Folder:
                    return FolderTemplate;
                default:
                    return RootTemplate;
            }
        }
    }
}
