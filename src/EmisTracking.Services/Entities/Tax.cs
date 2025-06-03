using EmisTracking.Services.Enums;
using System;

namespace EmisTracking.Services.Entities
{
    public class Tax : BaseEntity
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public double TotalAmount { get; set; }

        public DateTime CalculationDate { get; set; } = DateTime.Now;

        public HazardClass HazardClass { get; set; }

        public bool IsPaid { get; set; }
    }
}
