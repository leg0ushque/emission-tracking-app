using EmisTracking.Localization.StudentsPerf.Localization;
using EmisTracking.WebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class SourceSubstanceViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.EmissionSource)]
        public string EmissionSourceId { get; set; }

        [Display(Name = LangResources.Fields.Pollutant)]
        public string PollutantId { get; set; }

        [Display(Name = LangResources.Fields.IsRegulated)]
        public bool IsRegulated { get; set; }

        [Display(Name = LangResources.Fields.GasCleaningUnit)]
        public GasCleaningUnitType GasCleaningUnit { get; set; }

        [Display(Name = LangResources.Fields.PurificationPercentage)]
        public double PurificationPercentage { get; set; }

        [Display(Name = LangResources.Fields.AnnualAmount)]
        public double AnnualAmount { get; set; }
    }
}
