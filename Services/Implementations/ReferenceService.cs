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
    public class ReferenceService(ApplicationDbContext context, IMapper mapper) : IReferenceService
    {
        public async Task<List<ReferenceViewDto>> GetAllAsync()
        {
            var references = await context.References
                .Include(r => r.ReferenceType)
                .Include(r => r.User)
                .Include(r => r.Company)
                .Include(r => r.Department)
                .Include(r => r.Division)
                .Include(r => r.Employee)
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<ReferenceViewDto>>(references);
        }

        public async Task<ReferenceViewDto> GetByIdAsync(int id)
        {
            var reference = await context.References
                .Include(r => r.ReferenceType)
                .Include(r => r.User)
                .Include(r => r.Company)
                .Include(r => r.Department)
                .Include(r => r.Division)
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            return reference == null
                ? throw new ToException(ToErrors.REFERENCE_WITH_THIS_ID_NOT_FOUND)
                : mapper.Map<ReferenceViewDto>(reference);
        }

        public async Task<ReferenceViewDto> AddAsync(ReferenceCreateDto createDto)
        {
            var reference = mapper.Map<Reference>(createDto);

            if (!context.ReferenceTypes.Any(rt => rt.Id == createDto.ReferenceTypeId && !rt.IsDeleted))
                throw new ToException(ToErrors.REFERENCETYPE_WITH_THIS_ID_NOT_FOUND);

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

            await context.References.AddAsync(reference);
            await context.SaveChangesAsync();
            return mapper.Map<ReferenceViewDto>(reference);
        }

        public async Task<ReferenceViewDto> UpdateAsync(ReferenceUpdateDto updateDto)
        {
            var reference = await context.References.FindAsync(updateDto.Id);

            if (reference == null || reference.IsDeleted)
                throw new ToException(ToErrors.REFERENCE_WITH_THIS_ID_NOT_FOUND);

            mapper.Map(updateDto, reference);
            context.References.Update(reference);
            await context.SaveChangesAsync();
            return mapper.Map<ReferenceViewDto>(reference);
        }

        public async Task<ReferenceViewDto> DeleteAsync(int id)
        {
            var reference = await context.References.FindAsync(id);

            if (reference == null || reference.IsDeleted)
                throw new ToException(ToErrors.REFERENCE_WITH_THIS_ID_NOT_FOUND);

            reference.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<ReferenceViewDto>(reference);
        }

        public IQueryable<Reference> Search(string description = "", int page = 1, int size = 10)
        {
            return context.References
                .Include(r => r.ReferenceType)
                .Include(r => r.User)
                .Include(r => r.Company)
                .Include(r => r.Department)
                .Include(r => r.Division)
                .Include(r => r.Employee)
                .Where(r => !r.IsDeleted && (string.IsNullOrEmpty(description) || r.Description.Contains(description)))
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }
}