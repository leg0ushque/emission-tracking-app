namespace EmisTracking.Services.Entities
{
    public class SpecificIndicator : BaseEntity
    {
        public string ConsumptionGroupId { get; set; }
        public string PollutantId { get; set; }
        public double Value { get; set; }
    }
}
