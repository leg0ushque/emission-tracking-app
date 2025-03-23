using System.ComponentModel.DataAnnotations.Schema;

namespace EmisTracking.Services.Entities
{
    [Table("Departments")]
    public class Department : BaseEntity
    {
        public string AreaId { get; set; }
        public string Name { get; set; }
    }
}
