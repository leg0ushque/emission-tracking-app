using System.ComponentModel.DataAnnotations.Schema;

namespace EmisTracking.Services.Entities
{
    [Table("Areas")]
    public class Area : BaseEntity
    {
        public string Number { get; set; }

        public string Name { get; set; }
    }
}
