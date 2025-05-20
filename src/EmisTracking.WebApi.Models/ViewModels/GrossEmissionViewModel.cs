using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class GrossEmissionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.SourceSubstance)]
        public string SourceSubstanceId { get; set; }
        public SourceSubstanceViewModel SourceSubstance { get; set; }
        public IEnumerable<DropdownItemModel> SourceSubstances { get; set; }

        [Display(Name = LangResources.Fields.Methodology)]
        public string MethodologyId { get; set; }
        public MethodologyViewModel Methodology { get; set; }
        public IEnumerable<DropdownItemModel> Methodologies { get; set; }

        [Display(Name = LangResources.Fields.Mass)]
        public double Mass { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Tax)]
        public string TaxId { get; set; }
        public TaxViewModel Tax { get; set; }
        public IEnumerable<DropdownItemModel> Taxes { get; set; }
    }
}
