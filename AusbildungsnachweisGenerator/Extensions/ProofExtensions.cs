using AusbildungsnachweisGenerator.Model;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Extensions
{
    public static class ProofExtensions
    {
        public static void GenerateDocument(this Proof proof)
        {
            var document = new Document();
            document.LoadFromFile("Assets/ProofSample.docx");

            foreach (var item in proof.GetReplaceData())
            {
                var text = item.text ?? "";
                document.Replace(item.code, text, true, true);
            }

            document.SaveToFile(proof.FileName);
        }

        public static List<(string text, string code)> GetReplaceData(this Proof proof)
        {
            return new()
            {
                (proof.NoteNr, "{{noteNr}}"),
                (proof.Apprentice.FullName, "{{fullName}}"),
                (proof.Address.FullAddress, "{{fullAddress}}"),
                (proof.Job.Name, "{{jobName}}"),
                (proof.Job.Subject, "{{jobSubject}}"),
                (proof.Company.Name, "{{company}}"),
                (proof.Apprentice.FullName, "{{apprentice}}"),
                (proof.Apprenticeship.StartDate.ToString("d"), "{{appStart}}"),
                (proof.Apprenticeship.EndDate.ToString("d"), "{{appEnd}}"),
                (proof.Year.ToString(), "{{year}}"),
                (proof.Section, "{{section}}"),
                (proof.StartDate.ToString("d"), "{{weekStart}}"),
                (proof.EndDate.ToString("d"), "{{weekEnd}}"),
                (proof.HourRates[0].ToString(), "{{hours0}}"),
                (proof.HourRates[1].ToString(), "{{hours1}}"),
                (proof.HourRates[2].ToString(), "{{hours2}}"),
                (proof.HourRates[3].ToString(), "{{hours3}}"),
                (proof.HourRates[4].ToString(), "{{hours4}}"),
                (proof.Apprenticeship.WithSaturday ? $"{proof.HourRates[5]}" : "---", "{{hours5}}"),
                (proof.Apprenticeship.WithSaturday ? "" : "---", "{{saturday}}")
            };
        }
    }
}
