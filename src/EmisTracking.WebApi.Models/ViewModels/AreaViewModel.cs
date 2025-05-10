using EmisTracking.Localization.StudentsPerf.Localization;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class AreaViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Number)]
        public int Number { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }
    }
}
