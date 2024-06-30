using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using HRM_Project.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services.Implementations
{
    public class TimeSheetService(ApplicationDbContext context, IMapper mapper) : ITimeSheetService
    {
        public async Task<List<TimeSheetViewDto>> GetAllAsync()
        {
            var timeSheets = await context.TimeSheets
                .Include(t => t.Company)
                .Include(t => t.Department)
                .Include(t => t.Division)
                .Include(t => t.Employee)
                .Include(t => t.TimeSheetType)
                .Include(t => t.User)
                .Where(t => !t.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<TimeSheetViewDto>>(timeSheets);
        }

        public async Task<TimeSheetViewDto> GetByIdAsync(int id)
        {
            var timeSheet = await context.TimeSheets
                .Include(t => t.Company)
                .Include(t => t.Department)
                .Include(t => t.Division)
                .Include(t => t.Employee)
                .Include(t => t.TimeSheetType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            return timeSheet == null
                ? throw new ToException(ToErrors.TIMESHEET_WITH_THIS_ID_NOT_FOUND)
                : mapper.Map<TimeSheetViewDto>(timeSheet);
        }

        public async Task<TimeSheetViewDto> AddAsync(TimeSheetCreateDto createDto)
        {
            var timeSheet = mapper.Map<TimeSheet>(createDto);

            // Validate foreign key relationships
            if (createDto.CompanyId != 0 && !await context.Companies.AnyAsync(c => c.Id == createDto.CompanyId))
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

            if (createDto.DepartmentId != 0 && !await context.Departments.AnyAsync(d => d.Id == createDto.DepartmentId))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (createDto.DivisionId.HasValue && !await context.Divisions.AnyAsync(d => d.Id == createDto.DivisionId.Value))
                throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

            if (createDto.EmployeeId != 0 && !await context.Employees.AnyAsync(e => e.Id == createDto.EmployeeId))
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND);

            if (createDto.UserId.HasValue && !await context.Users.AnyAsync(u => u.Id == createDto.UserId.Value))
                throw new ToException(ToErrors.USER_WITH_THIS_ID_NOT_FOUND);

            await context.TimeSheets.AddAsync(timeSheet);
            await context.SaveChangesAsync();
            return mapper.Map<TimeSheetViewDto>(timeSheet);
        }

        public async Task<TimeSheetViewDto> UpdateAsync(TimeSheetUpdateDto updateDto)
        {
            var timeSheet = await context.TimeSheets.FindAsync(updateDto.Id);

            if (timeSheet == null || timeSheet.IsDeleted)
                throw new ToException(ToErrors.TIMESHEET_WITH_THIS_ID_NOT_FOUND);

            // Validate foreign key relationships
            if (updateDto.CompanyId != 0 && !await context.Companies.AnyAsync(c => c.Id == updateDto.CompanyId))
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

            if (updateDto.DepartmentId != 0 && !await context.Departments.AnyAsync(d => d.Id == updateDto.DepartmentId))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (updateDto.DivisionId.HasValue && !await context.Divisions.AnyAsync(d => d.Id == updateDto.DivisionId.Value))
                throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

            if (updateDto.EmployeeId != 0 && !await context.Employees.AnyAsync(e => e.Id == updateDto.EmployeeId))
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND);

            if (updateDto.UserId.HasValue && !await context.Users.AnyAsync(u => u.Id == updateDto.UserId.Value))
                throw new ToException(ToErrors.USER_WITH_THIS_ID_NOT_FOUND);

            mapper.Map(updateDto, timeSheet);
            context.TimeSheets.Update(timeSheet);
            await context.SaveChangesAsync();
            return mapper.Map<TimeSheetViewDto>(timeSheet);
        }

        public async Task<TimeSheetViewDto> DeleteAsync(int id)
        {
            var timeSheet = await context.TimeSheets.FindAsync(id);

            if (timeSheet == null || timeSheet.IsDeleted)
                throw new ToException(ToErrors.TIMESHEET_WITH_THIS_ID_NOT_FOUND);

            timeSheet.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<TimeSheetViewDto>(timeSheet);
        }

        public IQueryable<TimeSheet> Search(string numberTimeSheet = "", int page = 1, int size = 10)
        {
            return context.TimeSheets
                .Include(t => t.Company)
                .Include(t => t.Department)
                .Include(t => t.Division)
                .Include(t => t.Employee)
                .Include(t => t.TimeSheetType)
                .Include(t => t.User)
                .Where(t => !t.IsDeleted && (string.IsNullOrEmpty(numberTimeSheet) || t.NumberTimeSheet.Contains(numberTimeSheet)))
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }
}