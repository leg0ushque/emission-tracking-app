using EmisTracking.Services.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ServicesConstants = EmisTracking.Services.Constants;

namespace EmisTracking.Services.Database.Contexts
{
    public class UserDbContext(DbContextOptions<UserDbContext> options)
        : IdentityDbContext<SystemUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ROLES

            var adminRoleId = "1";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = ServicesConstants.AdminRole, NormalizedName = ServicesConstants.AdminRole.ToUpper() }
                //new IdentityRole { Id = "2", Name = ServicesConstants.EngineerRole, NormalizedName = ServicesConstants.EngineerRole.ToUpper() }
            );

            // FIRST USER

            var adminUser = new SystemUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = Constants.AdminMailboxLowerCase,
                FirstName = Constants.AdminFirstName,
                MiddleName = string.Empty,
                LastName = Constants.AdminLastName,
                Email = Constants.AdminMailboxLowerCase,
                NormalizedUserName = Constants.AdminMailboxLowerCase,
                NormalizedEmail = Constants.AdminMailboxLowerCase,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var hasher = new PasswordHasher<SystemUser>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, Constants.AdminInitialPassword);

            builder.Entity<SystemUser>().HasData(adminUser);

            // Add admin role to the first user!
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUser.Id
            });
        }
    }
}
