using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class ConsumptionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ConsumptionGroup)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string ConsumptionGroupId { get; set; }
        public ConsumptionGroupViewModel ConsumptionGroup { get; set; }
        public IEnumerable<DropdownItemModel> ConsumptionGroups { get; set; }

        [Display(Name = LangResources.Fields.Mass)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public double Mass { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Year { get; set; }
    }
}
