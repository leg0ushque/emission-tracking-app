using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class MethodologyParameter : BaseEntity
    {
        public string MethodologyId { get; set; }
        public virtual Methodology Methodology { get; set; }

        public ParameterType ParameterType { get; set; }

        public string Name { get; set; }

        public string FormulaName { get; set; }
    }
}
