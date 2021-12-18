using AusbildungsnachweisGenerator.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
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
        private string filePath;
        private Profile selectedProfile;
        private List<Profile> profiles;

        public DateTimeOffset StartDate 
        { 
            get => startDate;
            set
            {
                SetProperty(ref startDate, value);
                OnPropertyChanged(nameof(IsFormValid));
            }
        }
        public DateTimeOffset EndDate 
        { 
            get => endDate;
            set
            {
                SetProperty(ref endDate, value);
                OnPropertyChanged(nameof(IsFormValid));
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
                if(selectedProfile != null && !selectedProfile.IsForCreation)
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

        public void LoadProfiles()
        {
            Profiles = AppHelper.GetSettings().Profiles;
        }
    }
}
