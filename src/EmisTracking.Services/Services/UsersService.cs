using AutoMapper;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Exceptions;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmisTracking.Services.Services
{
    public interface IUsersService : IEntityService<User>
    {
        public Task<List<FullUser>> GetAllFullUsers(Expression<Func<SystemUser, bool>> predicate = null);

        public Task<List<FullUser>> GetByRoleAsync(string roleName, Expression<Func<User, bool>> predicate = null,
            params Expression<Func<User, object>>[] includes);

        public Task<User> GetBySystemUserId(string systemUserId, bool loadDependencies = false);
    }

    public class UsersService(IRepository<User> repository,
        UserManager<SystemUser> userManager,
        IMapper mapper)
        : GenericEntityService<User>(repository, mapper), IUsersService
    {
        private readonly UserManager<SystemUser> _userManager = userManager;

        protected override Expression<Func<User, object>>[] DependenciesIncludes => [ ];

        public async Task<List<FullUser>> GetAllFullUsers(Expression<Func<SystemUser, bool>> predicate = null)
        {
            var systemUsers = await (predicate != null ?
                 _userManager.Users.Where(predicate)
                : _userManager.Users).ToListAsync();

            var result = new List<FullUser>();

            foreach(var one in systemUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(one);
                var role = userRoles.FirstOrDefault();

                if (role != Constants.AdminRole)
                {
                    result.Add(CreateFullUserModel(one, role));
                }
            }

            return result;
        }

        public async Task<List<FullUser>> GetByRoleAsync(string roleName, Expression<Func<User, bool>> predicate = null,
            params Expression<Func<User, object>>[] includes)
        {
            if (string.IsNullOrEmpty(roleName))
                return new List<FullUser>();

            try
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

                var result = usersInRole.Select(x => CreateFullUserModel(x, roleName)).ToList();

                return result;
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public Task<User> GetBySystemUserId(string systemUserId, bool loadDependencies = false)
        {
            if (string.IsNullOrEmpty(systemUserId))
                return null;

            try
            {
                return _repository.GetAll(u => u.SystemUserId == systemUserId,
                        loadDependencies ? DependenciesIncludes : null)
                    .FirstOrDefaultAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        protected override Task ValidateAsync(User item)
        {
            return Task.CompletedTask;
        }

        private static FullUser CreateFullUserModel(SystemUser systemUser, string role)
            => new()
            {
                Id = systemUser.Id,
                SystemUserId = systemUser.Id,
                FirstName = systemUser.FirstName,
                LastName = systemUser.LastName,
                MiddleName = systemUser.MiddleName,
                Email = systemUser.Email,
                RoleName = role
            };
    }
}
