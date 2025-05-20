using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class OperatingTimeViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.EmissionSource)]
        public string EmissionSourceId { get; set; }
        public EmissionSourceViewModel EmissionSource { get; set; }
        public IEnumerable<DropdownItemModel> EmissionSources { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Hours)]
        public int Hours { get; set; }
    }
}
