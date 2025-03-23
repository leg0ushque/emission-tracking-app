using EmisTracking.WebApi.Enums;

namespace EmisTracking.WebApi.Models.ViewModels
{
    public class EmissionSourceViewModel : BaseViewModel
    {
        public string DepartmentId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public string RegimeId { get; set; }

        public ProcessCategory ProcessCategory { get; set; }
    }
}
