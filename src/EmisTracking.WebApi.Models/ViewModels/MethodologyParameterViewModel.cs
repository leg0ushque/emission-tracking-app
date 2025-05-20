using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyParameterViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }
        public MethodologyViewModel Methodology { get; set; }
        public IEnumerable<DropdownItemModel> Methodologies { get; set; }

        [Display(Name = LangResources.Fields.ParameterType)]
        public string ParameterType { get; set; }
    }
}
