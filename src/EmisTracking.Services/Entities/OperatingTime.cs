namespace EmisTracking.Services.Entities
{
    public class OperatingTime : BaseEntity
    {
        public string EmissionSourceId { get; set; }
        public virtual EmissionSource EmissionSource { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double Hours { get; set; }
    }
}
