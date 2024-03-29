﻿using AusbildungsnachweisGenerator.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public enum ProofType
    {
        [Path("Assets/Ausbildungsnachweis_Variable_Cover.docx")]
        Cover,
        [Path("Assets/Ausbildungsnachweis_Variable_Daily.docx")]
        Daily,
        [Path("Assets/Ausbildungsnachweis_Variable_Weekly.docx")]
        Weekly,
        [Path("Assets/Ausbildungsnachweis_Variable_DailyWithPlan.docx")]
        DailyWithPlan,
        [Path("Assets/Ausbildungsnachweis_Variable_WeeklyWithPlan.docx")]
        WeeklyWithPlan,
        [Path("Assets/Ausbildungsnachweis_Variable_Notice.docx")]
        Notice
    }
    public class Proof
    {
        public Proof(string noteNr, Apprenticeship apprenticeship, Apprentice apprentice, Address address, Instructor instructor, Job job, Company company, int year, DateTime startDate, DateTime endDate)
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

        public string FileName => $"{FileNameBase}{FileNameEnd(StartDate)}";
        public static string FileNameBase = "Ausbildungsnachweis_";
        public static string FileNameEnd(DateTime startDate) => startDate.ToString("d");

        public static ProofType GetProofTypeFromOptions(bool isDaily, bool isWeekly, bool hasPlan)
        {
            if (isDaily && isWeekly)
                throw new Exception("Proof Type cant be daily and weekly at the same time");

            if (isDaily)
            {
                if (hasPlan)
                    return ProofType.DailyWithPlan;
                else
                    return ProofType.Daily;
            }
            else if (isWeekly)
            {
                if (hasPlan)
                    return ProofType.WeeklyWithPlan;
                else
                    return ProofType.Weekly;
            }

            throw new Exception($"Proof Type was not selected or invalid ({isDaily}, {isWeekly}, {hasPlan})");
        }
        public static Proof Sample
        {
            get
            {
                var company = new Company("Musterfirma GmbH");
                var job = new Job("Fachinformatiker", "Anwendungsentwicklung", "Software");
                var instructor = new Instructor("Musterausbilder", "Thomas");
                var apprenticeAddress = new Address("Musterstr.", "12", "Musterstadt", "10010");
                var apprentice = new Apprentice("Musterazubi", "Max");
                var apprenticeship = new Apprenticeship(new(new(2020, 01, 01)), new(new(2023, 01, 01)), hourRate: 8);
                return new Proof("2", apprenticeship, apprentice, apprenticeAddress, instructor, job, company, 1, new(2021, 12, 13), new(2021, 12, 17));
            }
        }
    }
}
