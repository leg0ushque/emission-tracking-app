using EmisTracking.WebApi.Models.Models;
using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmisTracking.Services.WebApi.Services
{
    public interface IEmissionSourceApiService : IBaseApiService<EmissionSourceViewModel>
    {
        Task<ApiResponseModel<List<EmissionSourceViewModel>>> GetAllBySubdivisionAsync(string subdivisionId, bool loadDependencies = false);
    }
}