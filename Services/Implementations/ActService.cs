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
    public class ActService(ApplicationDbContext context, IMapper mapper) : IActService
    {
        public IQueryable<Act> Search(string actNumber = "", string description = "", int page = 1, int size = 10)
        {
            return context.Acts
                .Where(a => !a.IsDeleted &&
                            (string.IsNullOrEmpty(actNumber) || a.ActNumber.Contains(actNumber)) &&
                            (string.IsNullOrEmpty(description) || a.Description.Contains(description)))
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<ActViewDto>> GetActs()
        {
            return mapper.Map<List<ActViewDto>>(
                await context.Acts.Where(a => !a.IsDeleted).ToListAsync()
            );
        }

        public async Task<ActViewDto> GetByIdAsync(int id)
        {
            var act = await context.Acts.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

            return act == null ? throw new ToException(ToErrors.ACT_WITH_THIS_ID_NOT_FOUND) : mapper.Map<ActViewDto>(act);
        }

        public async Task<ActViewDto> AddAsync(ActCreateDto createDto)
        {
            // Check for required relations
            if (!context.ActTypes.Any(at => at.Id == createDto.ActTypeId && !at.IsDeleted))
                throw new ToException(ToErrors.ACTTYPE_WITH_THIS_ID_NOT_FOUND);

            if (createDto.UserId.HasValue && !context.Users.Any(u => u.Id == createDto.UserId && !u.IsDeleted))
                throw new ToException(ToErrors.USER_WITH_THIS_ID_NOT_FOUND);

            if (createDto.CompanyId.HasValue && !context.Companies.Any(c => c.Id == createDto.CompanyId && !c.IsDeleted))
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

            if (createDto.DepartmentId.HasValue && !context.Departments.Any(d => d.Id == createDto.DepartmentId && !d.IsDeleted))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (createDto.DivisionId.HasValue && !context.Divisions.Any(d => d.Id == createDto.DivisionId && !d.IsDeleted))
                throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

            if (createDto.EmployeeId.HasValue && !context.Employees.Any(e => e.Id == createDto.EmployeeId && !e.IsDeleted))
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND);

            var act = mapper.Map<Act>(createDto);
            await context.Acts.AddAsync(act);
            await context.SaveChangesAsync();
            return mapper.Map<ActViewDto>(act);
        }

        public async Task<ActViewDto> UpdateAsync(ActUpdateDto updateDto)
        {
            var act = await context.Acts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == updateDto.Id && !a.IsDeleted) ?? throw new ToException(ToErrors.ACT_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);
            act = mapper.Map<Act>(updateDto);
            context.Acts.Update(act);
            await context.SaveChangesAsync();
            return mapper.Map<ActViewDto>(act);
        }

        public async Task<ActViewDto> DeleteAsync(int id)
        {
            var act = await context.Acts.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted) ?? throw new ToException(ToErrors.ACT_WITH_THIS_ID_NOT_FOUND);
            act.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<ActViewDto>(act);
        }
    }
}