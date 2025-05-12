using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class SpecificIndicatorViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ConsumptionGroup)]
        public string ConsumptionGroupId { get; set; }

        [Display(Name = LangResources.Fields.Pollutant)]
        public string PollutantId { get; set; }

        [Display(Name = LangResources.Fields.Value)]
        public double Value { get; set; }
    }
}
