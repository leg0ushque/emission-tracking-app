using EmisTracking.Localization.StudentsPerf.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class SubdivisionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Area)]
        public string AreaId { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }
    }
}
