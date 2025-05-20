namespace EmisTracking.Services.Entities
{
    public class ConsumptionGroup : BaseEntity
    {
        public string MethodologyId { get; set; }
        public virtual Methodology Methodology { get; set; }

        public string Name { get; set; }
    }
}
