using EmisTracking.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyParameterViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }

        [Display(Name = LangResources.Fields.ParameterType)]
        public string ParameterType { get; set; }
    }
}
