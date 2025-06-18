using System.Collections.Generic;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class CalculationCheckResultViewModel : CalculationFormViewModel
    {
        public bool CanBeCalculated { get; set; }

        public List<CalculationParameterViewModel> Parameters { get; set; }

        public string ErrorMessage { get; set; }
    }

    public class CalculationResultViewModel : CalculationCheckResultViewModel
    {
        public List<GrossEmissionViewModel> GrossEmissions { get; set; }
    }
}
