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
                new IdentityRole { Id = adminRoleId, Name = ServicesConstants.AdminRole, NormalizedName = ServicesConstants.AdminRole.ToUpper() },
                new IdentityRole { Id = "2", Name = ServicesConstants.EcologistRole, NormalizedName = ServicesConstants.EcologistRole.ToUpper() },
                new IdentityRole { Id = "3", Name = ServicesConstants.AccountantRole, NormalizedName = ServicesConstants.AccountantRole.ToUpper() },
                new IdentityRole { Id = "4", Name = ServicesConstants.OperatorRole, NormalizedName = ServicesConstants.OperatorRole.ToUpper() },
                new IdentityRole { Id = "5", Name = ServicesConstants.DirectorRole, NormalizedName = ServicesConstants.DirectorRole.ToUpper() }
            );

            // FIRST USER

            var adminUser = new SystemUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = Constants.AdminMailbox,
                FirstName = Constants.AdminFirstName,
                MiddleName = string.Empty,
                LastName = Constants.AdminLastName,
                Email = Constants.AdminMailbox,
                NormalizedUserName = Constants.AdminMailbox,
                NormalizedEmail = Constants.AdminMailbox,
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
