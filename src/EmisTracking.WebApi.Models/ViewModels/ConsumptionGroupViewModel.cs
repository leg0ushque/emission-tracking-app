using EmisTracking.Localization.StudentsPerf.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class ConsumptionGroupViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }
    }
}
