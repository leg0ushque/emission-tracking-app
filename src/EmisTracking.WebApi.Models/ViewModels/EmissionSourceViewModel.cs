using EmisTracking.Localization.StudentsPerf.Localization;
using EmisTracking.WebApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class EmissionSourceViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Subdivision)]
        public string SubdivisionId { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.ProcessCategory)]
        public ProcessCategory ProcessCategory { get; set; }

        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }

        [Display(Name = LangResources.Fields.Mode)]
        public string ModeId { get; set; }
    }
}
