namespace EmisTracking.Services.Entities
{
    public class Consumption : BaseEntity
    {
        public string ConsumptionGroupId { get; set; }
        public double Mass { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
