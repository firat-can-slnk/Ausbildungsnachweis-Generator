using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Settings
    {
        public Job Job;
        public Instructor Instructor;
        public Company Company;
        public Apprenticeship Apprenticeship;
        public Apprentice Apprentice;
        public Address Address;

        public Settings(Job job, Instructor instructor, Company company, Apprenticeship apprenticeship, Apprentice apprentice, Address address)
        {
            Job = job;
            Instructor = instructor;
            Company = company;
            Apprenticeship = apprenticeship;
            Apprentice = apprentice;
            Address = address;
        }
    }
}
