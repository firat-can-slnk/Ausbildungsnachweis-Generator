using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var newDate = dt;

            while (newDate.DayOfWeek != startOfWeek)
                newDate = newDate.AddDays(-1);

            return newDate;
        }
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            var newDate = dt;

            while (newDate.AddDays(1).DayOfWeek != startOfWeek)
                newDate = newDate.AddDays(1);

            return newDate;
        }
        public static DateTime StartOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return dt.StartOfMonth().AddMonths(1).AddTicks(-1);
        }
        public static DateTime StartOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }
        public static DateTime EndOfYear(this DateTime dt)
        {
            return dt.StartOfYear().AddYears(1).AddTicks(-1);
        }
        public static IEnumerable<DateTime> GetWeeksInDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            startDate = startDate.StartOfWeek();

            while(startDate <= endDate.EndOfWeek())
            {
                yield return startDate;
                startDate = startDate.AddDays(7);
            }
        }
        public static IEnumerable<DateTime> GetMonthsInDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            startDate = startDate.StartOfMonth();

            while(startDate < endDate.EndOfMonth())
            {
                yield return startDate;
                startDate = startDate.AddMonths(1);
            }
        }
        public static IEnumerable<DateTime> GetYearsInDateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be greater than or equal to startDate");

            startDate = startDate.StartOfYear();

            while(startDate <= endDate.EndOfYear())
            {
                yield return startDate;
                startDate = startDate.AddYears(1);
            }
        }
    }
}
