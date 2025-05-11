using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public class UsersService : GenericEntityService<User>, IUsersService
    {
        public UsersService(IRepository<User> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        public Task<User> GetBySystemUserId(string systemUserId)
        {
            if (string.IsNullOrEmpty(systemUserId))
                return null;

            try
            {
                return _repository.GetAll(u => u.SystemUserId == systemUserId).FirstOrDefaultAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        protected override Task ValidateAsync(User item)
        {
            return Task.CompletedTask; // FIXME
        }
    }
}