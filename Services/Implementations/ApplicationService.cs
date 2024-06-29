using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services.Implementations
{
    public class ApplicationService(ApplicationDbContext context, IMapper mapper) : IApplicationService
    {
        public async Task<List<ApplicationViewDto>> GetAllAsync()
        {
            var applications = await context.Applications
                .Include(a => a.Company)
                .Include(a => a.Department)
                .Include(a => a.Division)
                .Include(a => a.Position)
                .Include(a => a.ApplicationType)
                .Include(a => a.Employee)
                .Include(a => a.User)
                .Where(a => !a.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<ApplicationViewDto>>(applications);
        }

        public async Task<ApplicationViewDto> GetByIdAsync(int id)
        {
            var application = await context.Applications
                .Include(a => a.Company)
                .Include(a => a.Department)
                .Include(a => a.Division)
                .Include(a => a.Position)
                .Include(a => a.ApplicationType)
                .Include(a => a.Employee)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            return application == null
                ? throw new ToException(ToErrors.APPLICATION_WITH_THIS_ID_NOT_FOUND)
                : mapper.Map<ApplicationViewDto>(application);
        }

        public async Task<ApplicationViewDto> AddAsync(ApplicationCreateDto createDto)
        {
            if (!context.Companies.Any(c => c.Id == createDto.CompanyId && !c.IsDeleted))
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

            if (!context.Users.Any(u => u.Id == createDto.UserId && !u.IsDeleted))
                throw new ToException(ToErrors.USER_WITH_THIS_ID_NOT_FOUND);

            if (createDto.DivisionId.HasValue && !context.Divisions.Any(d => d.Id == createDto.DivisionId && !d.IsDeleted))
                throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

            if (!context.Departments.Any(d => d.Id == createDto.DepartmentId && !d.IsDeleted))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (!context.Positions.Any(p => p.Id == createDto.PositionId && !p.IsDeleted))
                throw new ToException(ToErrors.POSITION_WITH_THIS_ID_NOT_FOUND);

            if (!context.ApplicationTypes.Any(at => at.Id == createDto.ApplicationTypeId && !at.IsDeleted))
                throw new ToException(ToErrors.APPLICATIONTYPE_WITH_THIS_ID_NOT_FOUND);

            if (!context.Employees.Any(e => e.Id == createDto.EmployeeId && !e.IsDeleted))
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND);

            var application = mapper.Map<Application>(createDto);
            await context.Applications.AddAsync(application);
            await context.SaveChangesAsync();
            return mapper.Map<ApplicationViewDto>(application);
        }


        public async Task<ApplicationViewDto> UpdateAsync(ApplicationUpdateDto updateDto)
        {
            var application = await context.Applications.FindAsync(updateDto.Id);

            if (application == null || application.IsDeleted)
                throw new ToException(ToErrors.APPLICATION_WITH_THIS_ID_NOT_FOUND);

            mapper.Map(updateDto, application);
            context.Applications.Update(application);
            await context.SaveChangesAsync();
            return mapper.Map<ApplicationViewDto>(application);
        }

        public async Task<ApplicationViewDto> DeleteAsync(int id)
        {
            var application = await context.Applications.FindAsync(id);

            if (application == null || application.IsDeleted)
                throw new ToException(ToErrors.APPLICATION_WITH_THIS_ID_NOT_FOUND);

            application.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<ApplicationViewDto>(application);
        }

        public IQueryable<Application> Search(string description = "", int page = 1, int size = 10)
        {
            return context.Applications
                .Include(a => a.Company)
                .Include(a => a.Department)
                .Include(a => a.Division)
                .Include(a => a.Position)
                .Include(a => a.ApplicationType)
                .Include(a => a.Employee)
                .Include(a => a.User)
                .Where(a => !a.IsDeleted && (string.IsNullOrEmpty(description) || a.Description.Contains(description)))
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }

}