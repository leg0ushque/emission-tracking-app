using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class OperatingTimeViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.EmissionSource)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string EmissionSourceId { get; set; }
        public EmissionSourceViewModel EmissionSource { get; set; }
        public IEnumerable<DropdownItemModel> EmissionSources { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Hours)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Hours { get; set; }
    }
}
