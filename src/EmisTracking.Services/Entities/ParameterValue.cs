namespace EmisTracking.Services.Entities
{
    public class ParameterValue : BaseEntity
    {
        public string MethodologyParameterId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Value { get; set; }
        public string GrossEmissionId { get; set; }
    }
}
