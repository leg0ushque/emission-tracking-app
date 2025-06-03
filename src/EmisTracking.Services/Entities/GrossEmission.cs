using System;

namespace EmisTracking.Services.Entities
{
    public class GrossEmission : BaseEntity
    {
        public string SourceSubstanceId { get; set; }
        public virtual SourceSubstance SourceSubstance { get; set; }

        public string MethodologyId { get; set; }
        public virtual Methodology Methodology { get; set; }

        public double Mass { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string TaxId { get; set; }
        public virtual Tax Tax { get; set; }

        public DateTime CalculationDate { get; set; } = DateTime.Now;
    }
}
