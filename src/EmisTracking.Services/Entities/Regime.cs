using System.ComponentModel.DataAnnotations.Schema;

namespace EmisTracking.Services.Entities
{
    [Table("Regimes")]
    public class Regime : BaseEntity
    {
        public string Name { get; set; }
    }
}
