using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using HRM_Project.Models.Types;
using HRM_Project.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services.Implementations
{
    public class TimeSheetTypeService(ApplicationDbContext context, IMapper mapper) : ITimeSheetTypeService
    {
        public async Task<List<TimeSheetTypeViewDto>> GetAllAsync()
        {
            var timeSheetTypes = await context.TimeSheetTypes
                .Where(t => !t.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<TimeSheetTypeViewDto>>(timeSheetTypes);
        }

        public async Task<TimeSheetTypeViewDto> GetByIdAsync(int id)
        {
            var timeSheetType = await context.TimeSheetTypes
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            return timeSheetType == null
                ? throw new ToException(ToErrors.TIMESHEETTYPE_WITH_THIS_ID_NOT_FOUND)
                : mapper.Map<TimeSheetTypeViewDto>(timeSheetType);
        }

        public async Task<TimeSheetTypeViewDto> AddAsync(TimeSheetTypeCreateDto createDto)
        {
            var timeSheetType = mapper.Map<TimeSheetType>(createDto);
            await context.TimeSheetTypes.AddAsync(timeSheetType);
            await context.SaveChangesAsync();
            return mapper.Map<TimeSheetTypeViewDto>(timeSheetType);
        }

        public async Task<TimeSheetTypeViewDto> UpdateAsync(TimeSheetTypeUpdateDto updateDto)
        {
            var timeSheetType = await context.TimeSheetTypes.FindAsync(updateDto.Id);

            if (timeSheetType == null || timeSheetType.IsDeleted)
                throw new ToException(ToErrors.TIMESHEETTYPE_WITH_THIS_ID_NOT_FOUND);

            mapper.Map(updateDto, timeSheetType);
            context.TimeSheetTypes.Update(timeSheetType);
            await context.SaveChangesAsync();
            return mapper.Map<TimeSheetTypeViewDto>(timeSheetType);
        }

        public async Task<TimeSheetTypeViewDto> DeleteAsync(int id)
        {
            var timeSheetType = await context.TimeSheetTypes.FindAsync(id);

            if (timeSheetType == null || timeSheetType.IsDeleted)
                throw new ToException(ToErrors.TIMESHEETTYPE_WITH_THIS_ID_NOT_FOUND);

            timeSheetType.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<TimeSheetTypeViewDto>(timeSheetType);
        }

        public IQueryable<TimeSheetType> Search(string name = "", int page = 1, int size = 10)
        {
            return context.TimeSheetTypes
                .Where(t => !t.IsDeleted && (string.IsNullOrEmpty(name) || t.Name.Contains(name)))
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }
}