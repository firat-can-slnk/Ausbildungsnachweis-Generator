using AusbildungsnachweisGenerator.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.ViewModel
{
    internal class SettingsPageViewModel : ObservableObject
    {
        private Job job;
        private Instructor instructor;
        private Company company;
        private Apprenticeship apprenticeship;
        private Apprentice apprentice;
        private Address address;

        public static Proof Sample { get; } = Proof.Sample;

        public Apprentice Apprentice
        {
            get => apprentice;
            set => SetProperty(ref apprentice, value);
        }
        public Address Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        public Apprenticeship Apprenticeship
        {
            get => apprenticeship;
            set => SetProperty(ref apprenticeship, value);
        }
        public Company Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }
        public Instructor Instructor
        {
            get => instructor;
            set => SetProperty(ref instructor, value);
        }
        public Job Job
        {
            get => job;
            set => SetProperty(ref job, value);
        }

        public SettingsPageViewModel()
        {
            var settings = AppHelper.GetSettings();
            job = settings?.Job ?? new();
            instructor = settings?.Instructor ?? new();
            company = settings?.Company ?? new();
            apprenticeship = settings?.Apprenticeship ?? new();
            apprentice = settings?.Apprentice ?? new();
            address = settings?.Address ?? new();
        }
    }
}
