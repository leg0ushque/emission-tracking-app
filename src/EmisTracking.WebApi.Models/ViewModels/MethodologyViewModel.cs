using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class MethodologyViewModel : BaseViewModel
    {
        // Для проверки парности квадратных скобок (без вложенности)
        private const string BalancedBracketsPattern = @"^[^\[\]]*(\[[^\[\]]*\][^\[\]]*)*$";

        // Для вывода списка параметров из формулы
        private const string FormulaParametersPattern = @"(?<=\[)[^\]]+(?=\])";

        [Display(Name = LangResources.Fields.ShortName)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string ShortName { get; set; }

        [Display(Name = LangResources.Fields.Name)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        public string Name { get; set; }

        [Display(Name = LangResources.Fields.Mode)]
        public string ModeId { get; set; }
        public ModeViewModel Mode { get; set; }
        public IEnumerable<DropdownItemModel> Modes { get; set; }

        [Display(Name = LangResources.Fields.Formula)]
        [Required(ErrorMessage = LangResources.MustBeFilledMessage)]
        [RegularExpression(BalancedBracketsPattern, ErrorMessage = LangResources.BracketsBalanceError)]
        public string Formula { get; set; }

        public List<string> GetFormulaParameters() =>
            Regex.Matches(Formula, FormulaParametersPattern)
                .Select(x => x.Value).Distinct().ToList();
    }
}
