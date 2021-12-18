using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Instructor
    {
        public Instructor()
        {
        }

        public Instructor(string name, string firstname)
        {
            Name = name;
            Firstname = firstname;
        }

        public string Name { get; set; }
        public string Firstname { get; set; }

        public string FullName => $"{Firstname} {Name}";
    }
}
