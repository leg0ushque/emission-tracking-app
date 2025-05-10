using EmisTracking.Localization.StudentsPerf.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.ShortName)]
        public string ShortName { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.Mode)]
        public string ModeId { get; set; }

        [Display(Name = LangResources.Fields.Formula)]
        public string Formula { get; set; }
    }
}
