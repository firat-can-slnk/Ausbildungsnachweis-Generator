using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Job
    {
        public Job()
        {
        }

        public Job(string name, string subject, string section)
        {
            Name = name;
            Subject = subject;
            Section = section;
        }

        public string Name { get; set; }
        public string Subject { get; set; }
        public string Section { get; set; }
    }
}
