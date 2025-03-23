using EmisTracking.Services.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmisTracking.Services.Entities
{
    [Table("EmissionSources")]
    public class EmissionSource : BaseEntity
    {
        public string DepartmentId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string RegimeId { get; set; }

        public ProcessCategory ProcessCategory { get; set; }
    }
}
