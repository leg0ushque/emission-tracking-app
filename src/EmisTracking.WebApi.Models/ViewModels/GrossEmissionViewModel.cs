using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class GrossEmissionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.SourceSubstance)]
        public string SourceSubstanceId { get; set; }

        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }

        [Display(Name = LangResources.Fields.Mass)]
        public double Mass { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Tax)]
        public string TaxId { get; set; }
    }
}
