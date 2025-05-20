using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class EmissionSourceViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.Subdivision)]
        public string SubdivisionId { get; set; }
        public SubdivisionViewModel Subdivision { get; set; }
        public IEnumerable<DropdownItemModel> Subdivisions { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.ProcessCategory)]
        public ProcessCategory ProcessCategory { get; set; }

        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }
        public MethodologyViewModel Methodology { get; set; }
        public IEnumerable<DropdownItemModel> Methodologies { get; set; }

        [Display(Name = LangResources.Fields.Mode)]
        public string ModeId { get; set; }
    }
}
