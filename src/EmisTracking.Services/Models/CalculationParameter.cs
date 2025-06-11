using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Models
{
    public class CalculationParameter
    {
        public string SourceSubstanceId { get; set; }
        public string SourceSubstanceName { get; set; }
        public string MethodologyParameterId { get; set; }
        public string ParameterValueId { get; set; }
        public ParameterType ParameterType { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public bool IsFilled { get; set; }
    }
}
