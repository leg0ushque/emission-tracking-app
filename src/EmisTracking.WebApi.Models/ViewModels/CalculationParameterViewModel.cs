using EmisTracking.WebApi.Models.Enums;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class CalculationParameterViewModel
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
