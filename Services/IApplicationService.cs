using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;

namespace HRM_Project.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationViewDto>> GetAllAsync();
        Task<ApplicationViewDto> GetByIdAsync(int id);
        Task<ApplicationViewDto> AddAsync(ApplicationCreateDto createDto);
        Task<ApplicationViewDto> UpdateAsync(ApplicationUpdateDto updateDto);
        Task<ApplicationViewDto> DeleteAsync(int id);
        IQueryable<Application> Search(string description = "", int page = 1, int size = 10);
    }
}