using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyParameterViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Methodology)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string MethodologyId { get; set; }
        public MethodologyViewModel Methodology { get; set; }
        public IEnumerable<DropdownItemModel> Methodologies { get; set; }

        [Display(Name = LangResources.Fields.ParameterType)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public ParameterType ParameterType { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.FormulaName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string FormulaName { get; set; }
    }
}
