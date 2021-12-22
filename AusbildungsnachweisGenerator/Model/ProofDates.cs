using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AusbildungsnachweisGenerator.Extensions;

namespace AusbildungsnachweisGenerator.Model
{
    public class ProofDates
    {
        public IEnumerable<DateTime> Years;
        public IEnumerable<DateTime> Months;
        public IEnumerable<DateTime> Weeks;
        public string RootPath;

        public ProofDates(DateTime startDate, DateTime endDate, string rootPath)
        {
            Weeks = DateTimeExtensions.GetWeeksInDateRange(startDate, endDate);
            Months = DateTimeExtensions.GetMonthsInDateRange(Weeks.First(), Weeks.Last().StartOfWeek());
            Years = DateTimeExtensions.GetYearsInDateRange(Weeks.First(), Weeks.Last().StartOfWeek());
            RootPath = rootPath;
        }

        public IEnumerable<DateTime> WeeksInMonth(DateTime month) => Weeks.Where(x => x.Month == month.Month && x.Year == month.Year);
        public IEnumerable<DateTime> MonthsInYear(DateTime year) => Months.Where(x => x.Year == year.Year);
        public string MonthDirectory(DateTime month) => $"{month.Month} {month:MMMM}";

        public string YearDirectory(DateTime year) => $"{year:yyyy}";
    }
}
