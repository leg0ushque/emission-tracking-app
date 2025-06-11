using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class ConsumptionGroupsService(IRepository<ConsumptionGroup> repository, IMapper mapper) : GenericEntityService<ConsumptionGroup>(repository, mapper)
    {
        protected override Expression<Func<ConsumptionGroup, object>>[] DependenciesIncludes =>
        [
            x => x.Methodology,
        ];

        protected override Task ValidateAsync(ConsumptionGroup item)
        {
            return Task.CompletedTask;
        }
    }
}