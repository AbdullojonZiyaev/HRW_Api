using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRM_Project.Services
{
    public interface ITimeSheetTypeService
    {
        Task<List<TimeSheetTypeViewDto>> GetAllAsync();
        Task<TimeSheetTypeViewDto> GetByIdAsync(int id);
        Task<TimeSheetTypeViewDto> AddAsync(TimeSheetTypeCreateDto createDto);
        Task<TimeSheetTypeViewDto> UpdateAsync(TimeSheetTypeUpdateDto updateDto);
        Task<TimeSheetTypeViewDto> DeleteAsync(int id);
        IQueryable<TimeSheetType> Search(string name = "", int page = 1, int size = 10);
    }
}