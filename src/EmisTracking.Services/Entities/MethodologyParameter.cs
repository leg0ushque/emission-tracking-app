using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class MethodologyParameter : BaseEntity
    {
        public string MethodologyId { get; set; }
        public ParameterType ParameterType { get; set; }
    }
}
