using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Common;

namespace HRM_Project.Services
{
    public interface ICityService
    {
        IQueryable<City> Search ( string name = "", int page = 1, int size = 10 );
        Task<List<CityViewDto>> GetCities ();
        Task<CityViewDto> GetByIdAsync ( int Id );
        Task<CityViewDto> AddAsync ( CityCreateDto city );
        Task<CityViewDto> UpdateAsync ( CityUpdateDto city );
        Task<CityViewDto> DeleteAsync ( int Id );
    }
}
