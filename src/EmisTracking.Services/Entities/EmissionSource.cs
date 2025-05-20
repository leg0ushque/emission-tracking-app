using EmisTracking.Services.Enums;

namespace EmisTracking.Services.Entities
{
    public class EmissionSource : BaseEntity
    {
        public string SubdivisionId { get; set; }
        public virtual Subdivision Subdivision { get; set; }

        public string Name { get; set; }

        public ProcessCategory ProcessCategory { get; set; }

        public string MethodologyId { get; set; }
        public virtual Methodology Methodology { get; set; }

        public string ModeId { get; set; }
        public virtual Mode Mode { get; set; }
    }
}
