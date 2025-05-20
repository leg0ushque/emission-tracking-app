using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class TaxRateViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.HazardClass)]
        public HazardClass HazardClass { get; set; }

        [Display(Name = LangResources.Fields.TaxRate)]
        public double Amount { get; set; }

        [Display(Name = LangResources.Fields.StartDate)]
        public DateTime StartDate { get; set; }

        [Display(Name = LangResources.Fields.EndDate)]
        public DateTime EndDate { get; set; }
    }
}
