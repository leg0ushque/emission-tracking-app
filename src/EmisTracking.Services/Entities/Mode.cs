using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class Mode : BaseEntity
    {
        public string Name { get; set; }
        public ProcessCategory ProcessCategory { get; set; }
    }
}
