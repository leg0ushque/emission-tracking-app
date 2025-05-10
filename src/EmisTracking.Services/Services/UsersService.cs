using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class UsersService : GenericEntityService<User>
    {
        public UsersService(IRepository<User> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        protected override Task ValidateAsync(User item)
        {
            return Task.CompletedTask;
        }
    }
}