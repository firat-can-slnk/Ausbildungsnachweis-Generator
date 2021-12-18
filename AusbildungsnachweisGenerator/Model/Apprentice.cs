using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Apprentice : ObservableObject
    {
        private string name;
        private string firstname;

        public Apprentice()
        {
        }

        public Apprentice(string name, string firstname)
        {
            Name = name;
            Firstname = firstname;
        }

        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Firstname { get => firstname; set => SetProperty(ref firstname, value); }
        public string FullName => $"{Firstname} {Name}";
    }
}
