using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class PollutantViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Name)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.Formula)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Formula { get; set; }

        [Display(Name = LangResources.Fields.HazardClass)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public HazardClass HazardClass { get; set; }

        [Display(Name = LangResources.Fields.AggregateState)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public AggregateState AggregateState { get; set; }
    }
}
