using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class ParameterValueViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.MethodologyParameter)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string MethodologyParameterId { get; set; }
        public MethodologyParameterViewModel MethodologyParameter { get; set; }
        public IEnumerable<DropdownItemModel> MethodologyParameters { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Value)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public double Value { get; set; }

        [Display(Name = LangResources.Fields.SourceSubstance)]
        public string SourceSubstanceId { get; set; }
        public SourceSubstanceViewModel SourceSubstance { get; set; }
        public IEnumerable<DropdownItemModel> SourceSubstances { get; set; }
    }
}
