using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class GrossEmissionViewModel : BaseViewModel
    {
        [Display(Name = LangResources.Fields.SourceSubstance)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string SourceSubstanceId { get; set; }
        public SourceSubstanceViewModel SourceSubstance { get; set; }
        public IEnumerable<DropdownItemModel> SourceSubstances { get; set; }

        [Display(Name = LangResources.Fields.Methodology)]
        [Required(ErrorMessage = LangResources.MustBeChosen)]
        public string MethodologyId { get; set; }
        public MethodologyViewModel Methodology { get; set; }
        public IEnumerable<DropdownItemModel> Methodologies { get; set; }

        [Display(Name = LangResources.Fields.Mass)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public double Mass { get; set; }

        [Display(Name = LangResources.Fields.Month)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Month { get; set; }

        [Display(Name = LangResources.Fields.Year)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public int Year { get; set; }

        [Display(Name = LangResources.Fields.Tax)]
        public string TaxId { get; set; }
        public TaxViewModel Tax { get; set; }
    }
}
