using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class SourceSubstance : BaseEntity
    {
        public string EmissionSourceId { get; set; }
        public virtual EmissionSource EmissionSource { get; set; }

        public string PollutantId { get; set; }
        public virtual Pollutant Pollutant { get; set; }

        public bool IsRegulated { get; set; }

        public GasCleaningUnitType GasCleaningUnitType { get; set; }

        public double PurificationPercentage { get; set; }

        public double AnnualAmount { get; set; }
    }
}
