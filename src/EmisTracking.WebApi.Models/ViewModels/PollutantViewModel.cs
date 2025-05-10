using EmisTracking.Localization.StudentsPerf.Localization;
using EmisTracking.WebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class PollutantViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.HazardClass)]
        public HazardClass HazardClass { get; set; }

        [Display(Name = LangResources.Fields.AggregateState)]
        public AggregateState AggregateState { get; set; }
    }
}
