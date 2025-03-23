using EmisTracking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmisTracking.Services.Entities
{
    public class SystemUser : IdentityUser, IEntity
    {
        [Required]
        public string FirstName { get; set; }

        public string FirstNameInitial => $"{FirstName[0]}.";

        public string MiddleName { get; set; }
        public string MiddleNameInitial => MiddleName.Length != 0 ? (MiddleName[0] + ".") : string.Empty;

        [Required]
        public string LastName { get; set; }

        public string FullName => $"{FirstNameInitial}{MiddleNameInitial}{LastName}";
    }
}
