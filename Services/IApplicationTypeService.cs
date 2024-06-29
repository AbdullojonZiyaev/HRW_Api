using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Types;

namespace HRM_Project.Services
{
        public interface IApplicationTypeService
        {
            Task<ApplicationTypeViewDto> AddAsync(ApplicationTypeCreateDto createDto);
            Task<ApplicationTypeViewDto> DeleteAsync(int id);
            Task<List<ApplicationTypeViewDto>> GetAllAsync();
            Task<ApplicationTypeViewDto> GetByIdAsync(int id);
            Task<ApplicationTypeViewDto> UpdateAsync(ApplicationTypeUpdateDto updateDto);
            IQueryable<ApplicationType> Search(string name = "", int page = 1, int size = 10);
    }

    }
