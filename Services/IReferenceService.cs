using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;

namespace HRM_Project.Services
{
    public interface IReferenceService
    {
        Task<List<ReferenceViewDto>> GetAllAsync();
        Task<ReferenceViewDto> GetByIdAsync(int id);
        Task<ReferenceViewDto> AddAsync(ReferenceCreateDto createDto);
        Task<ReferenceViewDto> UpdateAsync(ReferenceUpdateDto updateDto);
        Task<ReferenceViewDto> DeleteAsync(int id);
        IQueryable<Reference> Search(string description = "", int page = 1, int size = 10);
    }
}