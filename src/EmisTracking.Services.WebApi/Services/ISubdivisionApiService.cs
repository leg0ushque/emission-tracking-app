using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface ISubdivisionApiService : IBaseApiService<SubdivisionViewModel>
    {
        public Task<ApiResponseModel<List<SubdivisionViewModel>>> GetAllByAreaIdAsync(string areaId);
    }
}
