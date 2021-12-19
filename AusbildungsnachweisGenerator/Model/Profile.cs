using AusbildungsnachweisGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Profile
    {
        public static DateTime DefaultTimestamp = new(1900, 1, 1);
        public Job Job;
        public Instructor Instructor;
        public Company Company;
        public Apprenticeship Apprenticeship;
        public Apprentice Apprentice;
        public Address Address;
        public DateTime Timestamp;

        public Profile() { }
        public Profile(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        public static Profile New => new(DefaultTimestamp) { Apprenticeship = new(DateTime.Now.Date, DateTime.Now.Date.AddYears(3)) };

        public bool ValuesEmpty => (Job == null && Instructor == null && Company == null && Address == null && Apprentice == null) && (Apprenticeship?.Equals(New.Apprenticeship) ?? true);
        public bool IsForCreation => ValuesEmpty && Timestamp.Equals(DefaultTimestamp);

        public Profile(Job job, Instructor instructor, Company company, Apprenticeship apprenticeship, Apprentice apprentice, Address address, DateTime timestamp)
        {
            Job = job;
            Instructor = instructor;
            Company = company;
            Apprenticeship = apprenticeship;
            Apprentice = apprentice;
            Address = address;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            if (IsForCreation)
            {
                return "Neu";
            }
            else if (ValuesEmpty)
            {
                return "Leer";
            }
            else
            {
                return $"Azubi {(string.IsNullOrWhiteSpace(Apprentice?.FullName) ? "(leer)" : Apprentice?.FullName)} bei Firma {Company?.Name ?? "(leer)"}, erstellt am {Timestamp:g}";
            }
        }

        internal Proof GetProof(int noteNr, DateTime start, DateTime end)
        {
            return new Proof(noteNr.ToString(),
                                this.Apprenticeship,
                                this.Apprentice,
                                this.Address,
                                this.Instructor,
                                this.Job,
                                this.Company,
                                noteNr,
                                start,
                                end);
        }
    }
}
