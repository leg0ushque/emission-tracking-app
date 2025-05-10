namespace EmisTracking.Services.Entities
{
    public class GrossEmission : BaseEntity
    {
        public string SourceSubstanceId { get; set; }
        public string MethodologyId { get; set; }
        public double Mass { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string TaxId { get; set; }
    }
}
