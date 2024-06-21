using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using HRM_Project.Services.HRM_Project.Services;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class DivisionService(ApplicationDbContext context, IMapper mapper) : IDivisionService
    {
        public IQueryable<Division> Search(string fullname = "", int page = 1, int size = 10)
        {
            return context.Divisions.Include(d => d.Department)
                .Where(x => !x.IsDeleted && (string.IsNullOrEmpty(fullname) || x.Fullname.Contains(fullname.ToLower())))
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<DivisionViewDto>> GetDivisions()
        {
            return mapper.Map<List<DivisionViewDto>>(
                await context.Divisions.Where(d => !d.IsDeleted)
                    .Include(d => d.Department)
                    .Include(d => d.Employees)
                    .ToListAsync()
            );
        }

        public async Task<DivisionViewDto> AddAsync(DivisionCreateDto create)
        {
            if (!context.Departments.Any(d => d.Id == create.DepartmentId && !d.IsDeleted))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (context.Divisions.Any(d => !d.IsDeleted && d.Fullname == create.Fullname))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            Division division = mapper.Map<Division>(create);
            await context.AddAsync(division);
            await context.SaveChangesAsync();
            await context.Entry(division).Reference(d => d.Department).LoadAsync();
            return mapper.Map<DivisionViewDto>(division);
        }

        public async Task<DivisionViewDto> GetByIdAsync(int Id)
        {
            var entity = await context.Divisions
                .Include(x => x.Department)
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (entity == null) throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);
            return mapper.Map<DivisionViewDto>(entity);
        }

        public async Task<DivisionViewDto> UpdateAsync(DivisionUpdateDto update)
        {
            var res = await context.Divisions.AsNoTracking()
                .FirstOrDefaultAsync(d => !d.IsDeleted && d.Id == update.Id);

            if (res == null) throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);

            Division division = mapper.Map<Division>(update);
            context.Attach(division).State = EntityState.Modified;
            await context.SaveChangesAsync();
            await context.Entry(division).Reference(d => d.Department).LoadAsync();
            return mapper.Map<DivisionViewDto>(division);
        }

        public async Task<DivisionViewDto> DeleteAsync(int Id)
        {
            var entity = await context.Divisions
                .Include(x => x.Department)
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);

            if (entity == null)
                throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND);

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<DivisionViewDto>(entity);
        }

    }
}