using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public interface IUsersService : IEntityService<User>
    {
        public Task<User> GetBySystemUserId(string systemUserId);
    }
}