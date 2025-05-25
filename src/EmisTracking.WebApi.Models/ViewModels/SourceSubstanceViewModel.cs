using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class SourceSubstanceViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.EmissionSource)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string EmissionSourceId { get; set; }
        public EmissionSourceViewModel EmissionSource { get; set; }
        public IEnumerable<DropdownItemModel> EmissionSources { get; set; }


        [Display(Name = LangResources.Fields.Pollutant)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string PollutantId { get; set; }
        public PollutantViewModel Pollutant { get; set; }
        public IEnumerable<DropdownItemModel> Pollutants { get; set; }


        [Display(Name = LangResources.Fields.IsRegulated)]
        public bool IsRegulated { get; set; }


        [Display(Name = LangResources.Fields.GasCleaningUnitType)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public GasCleaningUnitType GasCleaningUnitType { get; set; }


        [Display(Name = LangResources.Fields.PurificationPercentage)]
        public double PurificationPercentage { get; set; }


        [Display(Name = LangResources.Fields.AnnualAmount)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public double AnnualAmount { get; set; }
    }
}
