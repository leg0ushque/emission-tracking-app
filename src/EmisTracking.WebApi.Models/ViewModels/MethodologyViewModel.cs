using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ShortName)]
        public string ShortName { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.Mode)]
        public string ModeId { get; set; }
        public ModeViewModel Mode { get; set; }
        public IEnumerable<DropdownItemModel> Modes { get; set; }

        [Display(Name = LangResources.Fields.Formula)]
        public string Formula { get; set; }
    }
}
