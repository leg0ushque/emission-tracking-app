namespace EmisTracking.Services.Entities
{
    public class Methodology : BaseEntity
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string ModeId { get; set; }
        public string Formula { get; set; }
    }
}
