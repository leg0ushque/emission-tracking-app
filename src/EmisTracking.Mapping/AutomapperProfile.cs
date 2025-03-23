using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.WebApi.Models.ViewModels;

namespace EmisTracking.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Area, AreaViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<EmissionSource, EmissionSource>().ReverseMap();
            CreateMap<Regime, RegimeViewModel>().ReverseMap();
        }
    }
}
