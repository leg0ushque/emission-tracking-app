using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class AreasService(IPaginatedRepository<Area> repository, IMapper mapper)
        : GenericEntityService<Area>(repository, mapper)
    {
        protected override Task ValidateAsync(Area item)
        {
            return Task.CompletedTask;
        }

        protected override Task ValidateDuplicateCreation(Area item)
        {
            return Task.CompletedTask;
        }
    }

    public class UsersService(IPaginatedRepository<User> repository, IMapper mapper)
        : GenericEntityService<User>(repository, mapper)
    {
        protected override Task ValidateAsync(User item)
        {
            return Task.CompletedTask;
        }

        protected override Task ValidateDuplicateCreation(User item)
        {
            return Task.CompletedTask;
        }
    }

    public class DepartmentsService(IPaginatedRepository<Department> repository, IMapper mapper)
        : GenericEntityService<Department>(repository, mapper)
    {
        protected override Task ValidateAsync(Department item)
        {
            return Task.CompletedTask;
        }

        protected override Task ValidateDuplicateCreation(Department item)
        {
            return Task.CompletedTask;
        }
    }

    public class RegimesService(IPaginatedRepository<Regime> repository, IMapper mapper)
        : GenericEntityService<Regime>(repository, mapper)
    {
        protected override Task ValidateAsync(Regime item)
        {
            return Task.CompletedTask;
        }

        protected override Task ValidateDuplicateCreation(Regime item)
        {
            return Task.CompletedTask;
        }
    }

    public class EmissionSourcesService(IPaginatedRepository<EmissionSource> repository, IMapper mapper)
        : GenericEntityService<EmissionSource>(repository, mapper)
    {
        protected override Task ValidateAsync(EmissionSource item)
        {
            return Task.CompletedTask;
        }

        protected override Task ValidateDuplicateCreation(EmissionSource item)
        {
            return Task.CompletedTask;
        }
    }
}