using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class OperatingTimeViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.EmissionSource)]
        public string EmissionSourceId { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Hours)]
        public int Hours { get; set; }
    }
}
