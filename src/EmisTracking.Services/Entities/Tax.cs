using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class Tax : BaseEntity
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public double TotalAmount { get; set; }

        public HazardClass HazardClass { get; set; }

        public bool IsPaid { get; set; }
    }
}
