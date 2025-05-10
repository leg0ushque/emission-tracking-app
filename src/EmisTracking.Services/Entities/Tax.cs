namespace EmisTracking.Services.Entities
{
    public class Tax : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double TotalAmount { get; set; }
        public string TaxRateId { get; set; }
        public bool IsPaid { get; set; }
    }
}
