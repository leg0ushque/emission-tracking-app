using EmisTracking.Localization;
using EmisTracking.WebApi.Models.Enums;
using EmisTracking.WebApi.Models.Models;
using System.Collections.Generic;
using System.Linq;
using Lang = EmisTracking.Localization.LangResources.EnumTranslations;

namespace EmisTracking.WebApp.Helpers
{
    public static class EnumHelper
    {
        public static Dictionary<int, string> Months
             => new()
             {
                 [1] = LangResources.Months.January,
                 [2] = LangResources.Months.February,
                 [3] = LangResources.Months.March,
                 [4] = LangResources.Months.April,
                 [5] = LangResources.Months.May,
                 [6] = LangResources.Months.June,
                 [7] = LangResources.Months.July,
                 [8] = LangResources.Months.August,
                 [9] = LangResources.Months.September,
                 [10] = LangResources.Months.October,
                 [11] = LangResources.Months.November,
                 [12] = LangResources.Months.December
             };

        public static Dictionary<AggregateState, string> AggregateStates
             => new()
             {
                 [AggregateState.Solid] = Lang.AggregateStateSolid,
                 [AggregateState.Liquid] = Lang.AggregateStateLiquid,
                 [AggregateState.Gas] = Lang.AggregateStateGas,
             };

        public static Dictionary<ProcessCategory, string> ProcessCategories
             => new()
             {
                 [ProcessCategory.None] = Lang.ProcessCategoryNone,
                 [ProcessCategory.BurningFuel] = Lang.ProcessCategoryBurningFuel,
                 [ProcessCategory.WasteProcessing] = Lang.ProcessCategoryWasteProcessing,
                 [ProcessCategory.AgriculturalObject] = Lang.ProcessCategoryAgriculturalObject,
                 [ProcessCategory.TechnologicalProcess] = Lang.ProcessCategoryTechnologicalProcess,
             };

        public static Dictionary<GasCleaningUnitType, string> GasCleaningUnitTypes
            => new()
            {
                [GasCleaningUnitType.Yes] = Lang.GasCleaningUnitTypeYes,
                [GasCleaningUnitType.No] = Lang.GasCleaningUnitTypeNo,
                [GasCleaningUnitType.Other] = Lang.GasCleaningUnitTypeOther,
            };

        public static Dictionary<HazardClass, string> HazardClasses
            => new()
            {
                [HazardClass.I] = Lang.HazardClassI,
                [HazardClass.II] = Lang.HazardClassII,
                [HazardClass.III] = Lang.HazardClassIII,
                [HazardClass.IV] = Lang.HazardClassIV,
            };

        public static Dictionary<ParameterType, string> ParameterTypes
            => new()
            {
                [ParameterType.Numeric] = Lang.ParameterTypeNumeric,
                [ParameterType.GasCleaningUnitPercent] = Lang.ParameterTypeGasCleaningUnitPercent,
                [ParameterType.ConsumptionMass] = Lang.ParameterTypeConsumptionMass,
                [ParameterType.OperatingTime] = Lang.ParameterTypeOperatingTime,
                [ParameterType.SpecificIndicator] = Lang.ParameterTypeSpecificIndicator,
            };

        public static List<DropdownItemModel> GetAggregateStatesDropdownValues()
             => [.. AggregateStates.Select(x => new DropdownItemModel(x.Key.ToString(), x.Value))];

        public static List<DropdownItemModel> GetProcessCategoriesDropdownValues()
             => [.. ProcessCategories.Select(x => new DropdownItemModel(x.Key.ToString(), x.Value))];

        public static List<DropdownItemModel> GetParameterTypeDropdownValues()
             => [.. ParameterTypes.Select(x => new DropdownItemModel(x.Key.ToString(), x.Value))];

        public static List<DropdownItemModel> GetGasCleaningUnitTypeDropdownValues()
             => [.. GasCleaningUnitTypes.Select(x => new DropdownItemModel(x.Key.ToString(), x.Value))];

        public static List<DropdownItemModel> GetHazardClassDropdownValues()
             => [.. HazardClasses.Select(x => new DropdownItemModel(x.Key.ToString(), x.Value))];
    }
}
