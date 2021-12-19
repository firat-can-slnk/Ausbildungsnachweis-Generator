using AusbildungsnachweisGenerator.Helper;
using AusbildungsnachweisGenerator.Model;
using Microsoft.UI.Xaml.Controls;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Extensions
{
    public static class ProofExtensions
    {
        public static void GenerateDocument(this Proof proof, string path, ProofType type)
        {
            var document = new Document();
            document.LoadFromFile(type.GetAttributeOfType<PathAttribute>().Path);

            foreach (var item in proof.GetReplaceData())
            {
                var text = item.text ?? "";
                document.Replace(item.code, text, true, true);
            }

            document.SaveToFile($@"{path}\{proof.FileName}.docx");
        }

        public static ObservableCollection<ProofTreeView> GetTreeViewNode(DateTime start, DateTime end)
        {
            var root = new ProofTreeView("Ausbildungsnachweise", ProofTreeViewType.Root);

            var weeks = start.GetWeeklyDateRangeTo(end);
            var months = start.GetMonthlyDateRangeTo(end);
            var years = start.GetYearlyDateRangeTo(end);

            foreach (var year in years)
            {
                var yearChild = new ProofTreeView(year.Year.ToString(), ProofTreeViewType.Folder);
                foreach (var month in months)
                {
                    if (month.Year != year.Year)
                        continue;

                    var monthChild = new ProofTreeView($"{month.Month} {month.ToString("MMMM")}", ProofTreeViewType.Folder);

                    foreach (var week in weeks)
                    {
                        if (week.Month != month.Month || week.Year != month.Year)
                            continue;

                        monthChild.Children.Add(new ProofTreeView($"{Proof.FileNameBase}{Proof.FileNameEnd(week)}.docx", ProofTreeViewType.File));
                    }

                    yearChild.Children.Add(monthChild);
                }

                root.Children.Add(yearChild);
            }

            return new() { root };
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
                (proof.Instructor.FullName, "{{instructor}}"),
                (proof.Apprenticeship.StartDate.ToString("d"), "{{appStart}}"),
                (proof.Apprenticeship.EndDate.ToString("d"), "{{appEnd}}"),
                (proof.Year.ToString(), "{{year}}"),
                (proof.Section, "{{section}}"),
                (proof.StartDate.ToString("d"), "{{weekStart}}"),
                (proof.EndDate.ToString("d"), "{{weekEnd}}"),
                (proof.HourRate?.ToString() ?? "", "{{hours}}"),
                (proof.Apprenticeship.WithSaturday ? $"{proof.HourRate?.ToString() ?? ""}" : "---", "{{hoursSat}}"),
                (proof.Apprenticeship.WithSaturday ? "" : "---", "{{saturday}}")
            };
        }
    }
}
