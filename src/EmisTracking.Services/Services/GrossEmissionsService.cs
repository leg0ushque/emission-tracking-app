using AutoMapper;
using EmisTracking.Localization;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Enums;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public interface IGrossEmissionService : IEntityService<GrossEmission>
    {
        public Task<CalculationCheckResult> CheckCalculationAsync(
            string methodologyId,
            string methodologyName,
            string emissionSourceId,
            string emissionSourceName,
            int month, int year);

        public Task<List<GrossEmission>> CalculateAsync(
            Methodology methodology,
            string emissionSourceId,
            string emissionSourceName,
            int month, int year);
    }

    public class GrossEmissionsService(
        ICalculationService calculationService,
        IRepository<GrossEmission> repository,
        IRepository<MethodologyParameter> methodologyParameterRepository,
        IRepository<ParameterValue> parameterValueRepository,
        IRepository<OperatingTime> operatingTimeService,
        IRepository<Consumption> consumptionService,
        IRepository<SourceSubstance> sourceSubstancesRepository,
        IRepository<SpecificIndicator> specificIndicatorService,
        IRepository<ConsumptionGroup> consumptionGroupRepository,
        IMapper mapper) : GenericEntityService<GrossEmission>(repository, mapper), IGrossEmissionService
    {
        private readonly ICalculationService calculationService = calculationService;
        private readonly IRepository<MethodologyParameter> _methodologyParameterRepository = methodologyParameterRepository;
        private readonly IRepository<ParameterValue> _parameterValueRepository = parameterValueRepository;
        private readonly IRepository<OperatingTime> _operatingTimeRepository = operatingTimeService;
        private readonly IRepository<Consumption> _consumptionRepository = consumptionService;
        private readonly IRepository<ConsumptionGroup> _consumptionGroupRepository = consumptionGroupRepository;
        private readonly IRepository<SourceSubstance> _sourceSubstancesRepository = sourceSubstancesRepository;
        private readonly IRepository<SpecificIndicator> _specificIndicatorRepository = specificIndicatorService;

        protected override Expression<Func<GrossEmission, object>>[] DependenciesIncludes =>
        [
            x => x.SourceSubstance,
            x => x.Methodology,
            x => x.Tax
        ];

        protected override Task ValidateAsync(GrossEmission item)
        {
            return Task.CompletedTask;
        }

        public override Task<List<GrossEmission>> GetAllAsync(
            Expression<Func<GrossEmission, bool>> predicate = null, bool loadDependencies = false)
        {
            try
            {
                return (loadDependencies ?
                    _repository.GetAll(predicate, DependenciesIncludes)
                    : _repository.GetAll(predicate))
                        .OrderByDescending(x => x.Year)
                        .ThenByDescending(x => x.Month)
                        .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public async Task<CalculationCheckResult> CheckCalculationAsync(
            string methodologyId,
            string methodologyName,
            string emissionSourceId,
            string emissionSourceName,
            int month, int year)
        {
            var methodologyParameters = await _methodologyParameterRepository
                .GetAll(p => p.MethodologyId == methodologyId)
                .ToListAsync();

            var sourceSubstances = await _sourceSubstancesRepository
                .GetAll(s => s.EmissionSourceId == emissionSourceId && s.IsRegulated,
                    includes: x => x.Pollutant)
                .ToListAsync();

            var sourcePollutantsIds = sourceSubstances.Select(x => x.Pollutant.Id).Distinct().ToList();

            var methodologyConsumptionGroups = await _consumptionGroupRepository
                .GetAll(g => g.MethodologyId == methodologyId)
                .ToListAsync();

            var methodologyConsumptionGroupsIds = methodologyConsumptionGroups.Select(g => g.Id).ToList();

            var methodologySpecificIndicators = new List<SpecificIndicator>();

            foreach (var item in methodologyConsumptionGroupsIds)
            {
                var groupIndicators = await _specificIndicatorRepository
                    .GetAll(i => i.ConsumptionGroupId == item,
                    includes: x => x.Pollutant)
                .ToListAsync();

                if (groupIndicators.Count != 0)
                    methodologySpecificIndicators.AddRange(groupIndicators);
            }

            var methodologyPollutantsIds = methodologySpecificIndicators.Distinct()
                .Select(x => x.Pollutant.Id)
                .Distinct()
                .ToList();

            var missingPollutants = methodologyPollutantsIds.Except(sourcePollutantsIds).ToList();

            if (missingPollutants.Count != 0)
            {
                var missingPollutantsDetails = methodologySpecificIndicators
                    .Where(i => missingPollutants.Contains(i.Pollutant.Id))
                    .Select(i => new KeyValuePair<string, string>(i.Pollutant.Id, i.Pollutant.Name))
                    .ToList();

                throw new BusinessLogicException(
                    $"{LangResources.MissingMethodologyPollutants}:\n{string.Join("; ", missingPollutantsDetails.Select(x => x.Value))}");
            }
            else
            {
                // Additional log here!
            }

            var parameters = new List<CalculationParameter>();

            var existingSourceParameters = new List<ParameterValue>();

            // Получить все параметры, связанные с веществами источника
            foreach (var sourceSubstance in sourceSubstances)
            {
                var substanceParameters = await _parameterValueRepository.GetAll(p =>
                    p.Month == month
                    && p.Year == year
                    && p.SourceSubstanceId == sourceSubstance.Id, includes: p => p.MethodologyParameter).ToListAsync();

                if (substanceParameters.Count != 0)
                {
                    existingSourceParameters.AddRange(substanceParameters);
                }
            }

            // а также параметры, не относящиеся к веществам
            var nonSubstanceParameters = await _parameterValueRepository.GetAll(p =>
                    p.Month == month
                    && p.Year == year
                    && p.SourceSubstanceId == null, includes: p => p.MethodologyParameter).ToListAsync();

            if (nonSubstanceParameters.Count != 0)
            {
                existingSourceParameters.AddRange(nonSubstanceParameters);
            }

            // Произвести обработку по каждому

            foreach (var methodologyParameter in methodologyParameters)
            {
                bool isSubstanceRelated = methodologyParameter.ParameterType == ParameterType.GasCleaningUnitPercent
                                         || methodologyParameter.ParameterType == ParameterType.ConsumptionMass
                                         || methodologyParameter.ParameterType == ParameterType.SpecificIndicator;

                var createdParameters = await FindOrCreateParameterAsync(
                        emissionSourceId,
                        methodologyParameter,
                        month,
                        year,
                        existingSourceParameters,
                        methodologyConsumptionGroups,
                        methodologySpecificIndicators,
                        isSubstanceRelated ? sourceSubstances : null);

                parameters.AddRange(createdParameters);
            }

            return new CalculationCheckResult
            {
                MethodologyId = methodologyId,
                MethodologyName = methodologyName,
                EmissionSourceId = emissionSourceId,
                EmissionSourceName = emissionSourceName,
                Month = month,
                Year = year,
                Parameters = parameters,
                CanBeCalculated = parameters.All(p => p.IsFilled)
            };
        }

        public async Task<List<GrossEmission>> CalculateAsync(
            Methodology methodology,
            string emissionSourceId,
            string emissionSourceName,
            int month, int year)
        {
            var checkResults = await CheckCalculationAsync(methodology.Id, methodology.Name, emissionSourceId, emissionSourceName, month, year);

            if (!checkResults.CanBeCalculated)
            {
                throw new BusinessLogicException(LangResources.CantBeCalculated);
            }

            var methodologyParameters = await _methodologyParameterRepository
                .GetAll(p => p.MethodologyId == methodology.Id)
                .ToListAsync();

            var sourceSubstances = await _sourceSubstancesRepository
                .GetAll(s => s.EmissionSourceId == emissionSourceId && s.IsRegulated,
                    includes: x => x.Pollutant)
                .ToListAsync();

            var grossEmissions = new List<GrossEmission>();

            foreach(var substance in sourceSubstances)
            {
                var parameters = checkResults.Parameters.Where(p =>
                    p.SourceSubstanceId == substance.Id || p.SourceSubstanceId == null)
                    .ToDictionary(x =>
                        methodologyParameters.FirstOrDefault(mp => mp.Id == x.MethodologyParameterId).FormulaName,
                        x => x.Value);

                var value = await calculationService.CalculateAsync(methodology.Formula, parameters);

                var emissionToCreate = new GrossEmission
                {
                    CalculationDate = DateTime.Now,
                    Mass = value,
                    MethodologyId = methodology.Id,
                    Methodology = methodology,
                    Month = month,
                    Year = year,
                    SourceSubstance = substance,
                    SourceSubstanceId = substance.Id,
                };

                emissionToCreate.Id = await _repository.CreateAsync(emissionToCreate);

                grossEmissions.Add(emissionToCreate);
            }

            return grossEmissions;
        }

        private async Task<List<CalculationParameter>> FindOrCreateParameterAsync(
            string emissionSourceId,
            MethodologyParameter parameter,
            int month, int year,
            List<ParameterValue> existingSourceParameters,
            List<ConsumptionGroup> methodologyConsumptionGroups,
            List<SpecificIndicator> methodologySpecificIndicators,
            List<SourceSubstance> sourceSubstances)
        {
            var result = new List<CalculationParameter>();

            switch (parameter.ParameterType)
            {
                case ParameterType.Numeric:
                    {
                        var existingValue = await _parameterValueRepository.GetAll(o =>
                            o.Year == year
                            && o.Month == month
                            && o.SourceSubstanceId == null
                            && o.MethodologyParameterId == parameter.Id).FirstOrDefaultAsync();

                        var calculationParameter = await ProcessParameterAsync(
                            parameter.Id,
                            sourceSubstanceId: null,
                            sourceSubstanceName: null,
                            parameter.Name,
                            month, year,
                            parameter.ParameterType,
                            valueFromTable: existingValue?.Value,
                            existingSourceParameters);

                        result.Add(calculationParameter);
                        break;
                    }
                case ParameterType.OperatingTime:
                    {
                        var existingOperatingTime = await _operatingTimeRepository.GetAll(o =>
                            o.Year == year
                            && o.Month == month
                            && o.EmissionSourceId == emissionSourceId).FirstOrDefaultAsync();

                        var calculationParameters = await ProcessParameterAsync(
                            parameter.Id,
                            sourceSubstanceId: null,
                            sourceSubstanceName: null,
                            parameter.Name,
                            month, year,
                            parameter.ParameterType,
                            valueFromTable: existingOperatingTime?.Hours,
                            existingSourceParameters);

                        result.Add(calculationParameters);
                        break;
                    }
                case ParameterType.GasCleaningUnitPercent:
                    {
                        foreach (var sourceSubstance in sourceSubstances)
                        {
                            var existingGcuPercent = sourceSubstance.PurificationPercentage;

                            var calculationParameters = await ProcessParameterAsync(
                                    parameter.Id,
                                    sourceSubstanceId: sourceSubstance.Id,
                                    sourceSubstanceName: sourceSubstance.Pollutant.Name,
                                    parameter.Name,
                                    month, year,
                                    parameter.ParameterType,
                                    valueFromTable: existingGcuPercent,
                                    existingSourceParameters);

                            result.Add(calculationParameters);
                        }

                        break;
                    }
                case ParameterType.ConsumptionMass:
                    {
                        foreach (var sourceSubstance in sourceSubstances)
                        {
                            double totalMass = Constants.ZeroMass;

                            foreach(var group in methodologyConsumptionGroups)
                            {
                                if(methodologySpecificIndicators.Any(s =>
                                    s.ConsumptionGroupId == group.Id
                                    && s.PollutantId == sourceSubstance.PollutantId))
                                {
                                    totalMass += await _consumptionRepository.GetAll(x =>
                                            x.Month == month && x.Year == year && x.ConsumptionGroupId == group.Id)
                                        .SumAsync(x => x.Mass);
                                }
                            }

                            var calculationParameters = await ProcessParameterAsync(
                                    parameter.Id,
                                    sourceSubstanceId: sourceSubstance.Id,
                                    sourceSubstanceName: sourceSubstance.Pollutant.Name,
                                    parameter.Name,
                                    month, year,
                                    parameter.ParameterType,
                                    valueFromTable: totalMass,
                                    existingSourceParameters);

                            result.Add(calculationParameters);
                        }

                        break;
                    }
                case ParameterType.SpecificIndicator:
                    {
                        foreach (var sourceSubstance in sourceSubstances)
                        {
                            var foundSpecificIndicator = methodologySpecificIndicators.FirstOrDefault(i =>
                                i.PollutantId == sourceSubstance.Id);

                            var calculationParameters = await ProcessParameterAsync(
                                    parameter.Id,
                                    sourceSubstanceId: null,
                                    sourceSubstanceName: null,
                                    parameter.Name,
                                    month, year,
                                    parameter.ParameterType,
                                    valueFromTable: foundSpecificIndicator.Value,
                                    existingSourceParameters);

                            result.Add(calculationParameters);
                        }

                        break;
                    }
                default:
                    throw new BusinessLogicException(
                        string.Format(LangResources.UnknownParameterTypeTemplate, parameter.ParameterType, parameter.Id));
            }

            return result;
        }

        private async Task<CalculationParameter> ProcessParameterAsync(
            string methodologyParameterId,
            string sourceSubstanceId,
            string sourceSubstanceName,
            string parameterName,
            int month,
            int year,
            ParameterType parameterType,
            double? valueFromTable,
            IEnumerable<ParameterValue> existingSourceParameters)
        {
            var existingParameterQuery = existingSourceParameters.Where(p =>
                p.MethodologyParameter.Id == methodologyParameterId
                && p.Month == month
                && p.Year == year);

            var existingParameter = sourceSubstanceId != null ?
                existingParameterQuery.FirstOrDefault(p => p.SourceSubstanceId == sourceSubstanceId)
                : existingParameterQuery.FirstOrDefault();

            if (valueFromTable != null)
            {
                if (existingParameter == null)
                {
                    existingParameter = new ParameterValue
                    {
                        MethodologyParameterId = methodologyParameterId,
                        SourceSubstanceId = sourceSubstanceId,
                        Month = month,
                        Year = year,
                        Value = valueFromTable.Value,
                    };

                    existingParameter.Id = await _parameterValueRepository.CreateAsync(existingParameter);
                }
                else if (Math.Abs(existingParameter.Value - valueFromTable.Value) > Constants.Epsilon)
                {
                    existingParameter.Value = valueFromTable.Value;

                    await _parameterValueRepository.UpdateAsync(existingParameter);
                }
            }

            return new CalculationParameter
            {
                IsFilled = existingParameter != null && valueFromTable != null,
                SourceSubstanceId = sourceSubstanceId,
                SourceSubstanceName = sourceSubstanceName ?? string.Empty,
                MethodologyParameterId = methodologyParameterId,
                Name = parameterName,
                ParameterType = parameterType,
                ParameterValueId = existingParameter?.Id,
                Value = existingParameter != null ? existingParameter.Value : Constants.DefaultParameterValue,
            };
        }
    }
}