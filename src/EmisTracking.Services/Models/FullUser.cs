using System.Text.RegularExpressions;

namespace EmisTracking.Services.Models
{
    public class FullUser
    {
        public string Id { get; set; }

        public string SystemUserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public bool? IsDisabled { get; set; }

        public string Info { get; set; }

        public string GroupId { get; set; }

        public Group Group { get; set; }

        public string RoleName { get; set; }
    }
}
