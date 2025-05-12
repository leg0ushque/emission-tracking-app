using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class ParameterValueViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.MethodologyParameter)]
        public string MethodologyParameterId { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Value)]
        public double Value { get; set; }

        [Display(Name = LangResources.Fields.GrossEmission)]
        public string GrossEmissionId { get; set; }
    }
}
