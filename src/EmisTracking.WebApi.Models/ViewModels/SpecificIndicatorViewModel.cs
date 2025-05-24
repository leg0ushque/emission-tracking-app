using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class SpecificIndicatorViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ConsumptionGroup)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string ConsumptionGroupId { get; set; }
        public ConsumptionGroupViewModel ConsumptionGroup { get; set; }
        public IEnumerable<DropdownItemModel> ConsumptionGroups { get; set; }


        [Display(Name = LangResources.Fields.Pollutant)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string PollutantId { get; set; }
        public PollutantViewModel Pollutant { get; set; }
        public IEnumerable<DropdownItemModel> Pollutants { get; set; }


        [Display(Name = LangResources.Fields.Value)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public double Value { get; set; }
    }
}
