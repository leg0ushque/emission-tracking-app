using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class ConsumptionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ConsumptionGroup)]
        public string ConsumptionGroupId { get; set; }
        public ConsumptionGroupViewModel ConsumptionGroup { get; set; }
        public IEnumerable<DropdownItemModel> ConsumptionGroups { get; set; }

        [Display(Name = LangResources.Fields.Mass)]
        public double Mass { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }
    }
}
