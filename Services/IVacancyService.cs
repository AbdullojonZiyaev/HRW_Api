using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;

namespace HRM_Project.Services
{
    public interface IVacancyService
    {
        Task<List<VacancyViewDto>> GetAllAsync();
        Task<VacancyViewDto> GetByIdAsync(int id);
        Task<VacancyViewDto> AddAsync(VacancyCreateDto createDto);
        Task<VacancyViewDto> UpdateAsync(VacancyUpdateDto updateDto);
        Task<VacancyViewDto> DeleteAsync(int id);
        IQueryable<Vacancy> Search(string title = "", int page = 1, int size = 10);
    }

}
