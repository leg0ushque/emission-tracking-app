using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class TaxRateViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.HazardClass)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public HazardClass HazardClass { get; set; }

        [Display(Name = LangResources.Fields.TaxRate)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public double Amount { get; set; }

        [Display(Name = LangResources.Fields.StartDate)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = LangResources.Fields.EndDate)]
        public DateTime? EndDate { get; set; }
    }
}
