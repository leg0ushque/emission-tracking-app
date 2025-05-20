using EmisTracking.Services.Enums;
using System;

namespace EmisTracking.Services.Entities
{
    public class TaxRate : BaseEntity
    {
        public HazardClass HazardClass { get; set; }

        public double Amount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
