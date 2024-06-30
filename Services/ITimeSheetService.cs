using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;


namespace HRM_Project.Services
{
    public interface ITimeSheetService
    {
        Task<List<TimeSheetViewDto>> GetAllAsync();
        Task<TimeSheetViewDto> GetByIdAsync(int id);
        Task<TimeSheetViewDto> AddAsync(TimeSheetCreateDto createDto);
        Task<TimeSheetViewDto> UpdateAsync(TimeSheetUpdateDto updateDto);
        Task<TimeSheetViewDto> DeleteAsync(int id);
        IQueryable<TimeSheet> Search(string numberTimeSheet = "", int page = 1, int size = 10);
    }
}