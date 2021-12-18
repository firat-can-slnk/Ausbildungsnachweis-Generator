using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Proof
    {
        public Proof(string noteNr, Apprenticeship apprenticeship, Apprentice apprentice, Address address, Instructor instructor, Job job, Company company, int year, DateTime startDate, DateTime endDate, string section = null, int? hourRate = null)
        {
            NoteNr = noteNr;
            Apprenticeship = apprenticeship;
            Apprentice = apprentice;
            Address = address;
            Instructor = instructor;
            Job = job;
            Company = company;
            Year = year;
            StartDate = startDate;
            EndDate = endDate;
            Section = section ?? "";
            HourRates =  Enumerable.Repeat(hourRate ?? 8, 6).ToList();
        }

        public string NoteNr { get; set; }
        public Apprenticeship Apprenticeship { get; set; }
        public Apprentice Apprentice { get; set; }
        public Address Address { get; set; }
        public Instructor Instructor { get; set; }
        public Job Job { get; set; }
        public Company Company { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Section { get; set; }
        public List<int> HourRates { get; set; }

        public string FileName => $"Ausbildungsnachweis_{StartDate:d}";

        public static Proof Sample
        {
            get
            {
                var company = new Company("Musterfirma GmbH");
                var job = new Job("Fachinformatiker", "Anwendungsentwicklung");
                var instructor = new Instructor("Musterausbilder", "Thomas");
                var apprenticeAddress = new Address("Musterstr.", "12", "Musterstadt", "10010");
                var apprentice = new Apprentice("Musterazubi", "Max");
                var apprenticeship = new Apprenticeship(new(new(2020, 01, 01)), new(new(2023, 01, 01)));
                return new Proof("1", apprenticeship, apprentice, apprenticeAddress, instructor, job, company, 2, new(2021, 12, 13), new(2021, 12, 17));
            }
        }
    }
}
