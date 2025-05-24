using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ShortName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string ShortName { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.Mode)]
        public string ModeId { get; set; }
        public ModeViewModel Mode { get; set; }
        public IEnumerable<DropdownItemModel> Modes { get; set; }

        [Display(Name = LangResources.Fields.Formula)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Formula { get; set; }
    }
}
