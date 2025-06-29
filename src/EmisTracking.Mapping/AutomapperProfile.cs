﻿using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Models;
using EmisTracking.WebApi.Models.ViewModels;

namespace EmisTracking.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Area, AreaViewModel>().ReverseMap();
            CreateMap<Subdivision, SubdivisionViewModel>().ReverseMap();
            CreateMap<Mode, ModeViewModel>().ReverseMap();
            CreateMap<Methodology, MethodologyViewModel>().ReverseMap();
            CreateMap<EmissionSource, EmissionSourceViewModel>().ReverseMap();
            CreateMap<OperatingTime, OperatingTimeViewModel>().ReverseMap();
            CreateMap<Pollutant, PollutantViewModel>().ReverseMap();
            CreateMap<SourceSubstance, SourceSubstanceViewModel>().ReverseMap();
            CreateMap<MethodologyParameter, MethodologyParameterViewModel>().ReverseMap();
            CreateMap<ConsumptionGroup, ConsumptionGroupViewModel>().ReverseMap();
            CreateMap<SpecificIndicator, SpecificIndicatorViewModel>().ReverseMap();
            CreateMap<Consumption, ConsumptionViewModel>().ReverseMap();
            CreateMap<ParameterValue, ParameterValueViewModel>().ReverseMap();
            CreateMap<GrossEmission, GrossEmissionViewModel>().ReverseMap();
            CreateMap<TaxRate, TaxRateViewModel>().ReverseMap();
            CreateMap<Tax, TaxViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, FullUser>().ReverseMap();
            CreateMap<UserViewModel, FullUser>().ReverseMap();
            CreateMap<SystemUser, FullUser>().ReverseMap();
            CreateMap<CalculationCheckResult, CalculationCheckResultViewModel>().ReverseMap();

            CreateMap<Services.Enums.AggregateState, WebApi.Models.Enums.AggregateState>()
                .ConvertUsingEnumMapping()
                .ReverseMap();
            CreateMap<Services.Enums.GasCleaningUnitType, WebApi.Models.Enums.GasCleaningUnitType>()
                .ConvertUsingEnumMapping()
                .ReverseMap();
            CreateMap<Services.Enums.HazardClass, WebApi.Models.Enums.HazardClass>()
                .ConvertUsingEnumMapping()
                .ReverseMap();
            CreateMap<Services.Enums.ParameterType, WebApi.Models.Enums.ParameterType>()
                .ConvertUsingEnumMapping()
                .ReverseMap();
            CreateMap<Services.Enums.ProcessCategory, WebApi.Models.Enums.ProcessCategory>()
                .ConvertUsingEnumMapping()
                .ReverseMap();
        }
    }
}
