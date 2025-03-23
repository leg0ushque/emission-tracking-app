using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmisTracking.Services.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        public string SystemUserId { get; set; }

        public string Info { get; set; }
    }
}
