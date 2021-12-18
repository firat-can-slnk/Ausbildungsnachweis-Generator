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

        public Job(string name, string subject)
        {
            Name = name;
            Subject = subject;
        }

        public string Name { get; set; }
        public string Subject { get; set; }
    }
}
