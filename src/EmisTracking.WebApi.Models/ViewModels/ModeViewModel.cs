using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class ModeViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Name)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.ProcessCategory)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public ProcessCategory ProcessCategory { get; set; }
    }
}
