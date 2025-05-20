using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class Pollutant : BaseEntity
    {
        public string Name { get; set; }

        public string Formula { get; set; }

        public HazardClass HazardClass { get; set; }

        public AggregateState AggregateState { get; set; }
    }
}
