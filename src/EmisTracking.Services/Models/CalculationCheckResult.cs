using System.Collections.Generic;

namespace EmisTracking.Services.Models
{
    public class CalculationCheckResult
    {
        public string MethodologyId { get; set; }
        public string MethodologyName { get; set; }
        public string EmissionSourceId { get; set; }
        public string EmissionSourceName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool CanBeCalculated { get; set; }
        public bool CalculatedSuccessfully { get; set; }

        public List<CalculationParameter> Parameters { get; set; }
    }
}
