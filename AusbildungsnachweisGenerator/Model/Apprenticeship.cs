using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Apprenticeship : IEquatable<Apprenticeship>
    {
        public Apprenticeship()
        {
        }

        public Apprenticeship(DateTimeOffset startDate, DateTimeOffset endDate, bool withSaturday = false, int hourRate = 8)
        {
            StartDate = startDate;
            EndDate = endDate;
            WithSaturday = withSaturday;
            HourRate = hourRate;
        }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool WithSaturday { get; set; }
        public int HourRate { get; set; }

        public bool Equals(Apprenticeship obj)
        {
            return StartDate == obj.StartDate && EndDate == obj.EndDate && WithSaturday == obj.WithSaturday && HourRate == obj.HourRate;
        }
    }
}
