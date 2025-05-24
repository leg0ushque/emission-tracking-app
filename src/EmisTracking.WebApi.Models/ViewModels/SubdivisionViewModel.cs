using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class SubdivisionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Area)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string AreaId { get; set; }
        public AreaViewModel Area { get; set; }
        public IEnumerable<DropdownItemModel> Areas { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Name { get; set; }
    }
}
