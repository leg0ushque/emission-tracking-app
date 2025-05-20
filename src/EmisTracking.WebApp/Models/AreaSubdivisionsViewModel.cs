using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;

namespace EmisTracking.WebApp.Models
{
    public class AreaSubdivisionsViewModel
    {
        public AreaViewModel Area { get; set; }

        public IEnumerable<SubdivisionViewModel> Subdivisions { get; set; }
    }
}
