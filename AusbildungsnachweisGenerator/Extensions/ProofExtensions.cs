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


        public static string GenerateDocument(this Proof proof, string path, ProofType type)
        {
            var document = new Document();
            document.LoadFromFile(type.GetAttributeOfType<PathAttribute>().Path);

            foreach (var item in proof.GetReplaceData())
            {
                var text = item.text ?? "";
                document.Replace(item.code, text, true, true);
            }

            var filePath = $@"{path}\{proof.FileName}.docx";
            document.SaveToFile(filePath);
            return filePath;
        }

        public static ObservableCollection<ProofTreeView> GetTreeViewNode(DateTime start, DateTime end)
        {
            var root = new ProofTreeView("Ausbildungsnachweise", ProofTreeViewType.Root);

            var dates = new ProofDates(start, end, "");

            // loop each year
            foreach (var year in dates.Years)
            {
                var yearChild = new ProofTreeView(dates.YearDirectory(year), ProofTreeViewType.Folder);

                // loop each month of that year year
                foreach (var month in dates.MonthsInYear(year))
                {
                    var monthChild = new ProofTreeView(dates.MonthDirectory(month), ProofTreeViewType.Folder);

                    // loop each week of the month in the year
                    foreach (var week in dates.WeeksInMonth(month))
                    {
                        // Add week node
                        monthChild.Children.Add(new ProofTreeView($"{Proof.FileNameBase}{Proof.FileNameEnd(week)}.docx", ProofTreeViewType.File));
                    }

                    // Add month node
                    yearChild.Children.Add(monthChild);
                }

                // Add year node
                root.Children.Add(yearChild);
            }

            return new() { root };
        }

        public static List<(string text, string code)> GetReplaceData(this Proof proof) => new()
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
            (proof.Job.Section, "{{section}}"),
            (proof.StartDate.ToString("d"), "{{weekStart}}"),
            (proof.EndDate.ToString("d"), "{{weekEnd}}"),
            (proof.Apprenticeship.HourRate?.ToString() ?? "", "{{hours}}"),
            (proof.Apprenticeship.WithSaturday ? $"{proof.Apprenticeship.HourRate?.ToString() ?? ""}" : "---", "{{hoursSat}}"),
            (proof.Apprenticeship.WithSaturday ? "" : "---", "{{saturday}}")
        };
    }
}
