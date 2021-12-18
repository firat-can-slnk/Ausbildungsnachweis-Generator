using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Apprentice
    {
        public Apprentice()
        {
        }

        public Apprentice(string name, string firstname)
        {
            Name = name;
            Firstname = firstname;
        }

        public string Name { get; set; }
        public string Firstname { get; set; }
        public string FullName => $"{Firstname} {Name}";
    }
}
