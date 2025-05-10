using EmisTracking.Localization.StudentsPerf.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class TaxViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.TaxAmount)]
        public double TotalAmount { get; set; }

        [Display(Name = LangResources.Fields.TaxRate)]
        public string TaxRateId { get; set; }

        [Display(Name = LangResources.Fields.IsPaid)]
        public bool IsPaid { get; set; }
    }
}
