using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Types;

namespace HRM_Project.Services
{
    public interface IReferenceTypeService
    {
        Task<List<ReferenceTypeViewDto>> GetAllAsync();
        Task<ReferenceTypeViewDto> GetByIdAsync(int id);
        Task<ReferenceTypeViewDto> AddAsync(ReferenceTypeCreateDto createDto);
        Task<ReferenceTypeViewDto> UpdateAsync(ReferenceTypeUpdateDto updateDto);
        Task<ReferenceTypeViewDto> DeleteAsync(int id);
        IQueryable<ReferenceType> Search(string name = "", int page = 1, int size = 10);
    }
}