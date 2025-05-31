namespace EmisTracking.Services.Entities
{
    public class SpecificIndicator : BaseEntity
    {
        public string Name { get; set; }

        public string ConsumptionGroupId { get; set; }
        public virtual ConsumptionGroup ConsumptionGroup { get; set; }

        public string PollutantId { get; set; }
        public virtual Pollutant Pollutant { get; set; }

        public double Value { get; set; }
    }
}
