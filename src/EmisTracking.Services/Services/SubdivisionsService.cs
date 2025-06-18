using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class SubdivisionsService(IRepository<Subdivision> repository, IMapper mapper) : GenericEntityService<Subdivision>(repository, mapper)
    {
        protected override Expression<Func<Subdivision, object>>[] DependenciesIncludes =>
        [
            x => x.Area,
        ];

        protected override Task ValidateAsync(Subdivision item)
        {
            return Task.CompletedTask;
        }
    }
}