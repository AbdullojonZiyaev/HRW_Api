namespace HRM_Project.Services.Implementations
{
    using AutoMapper;
    using global::HRM_Project.DTOs.Request;
    using global::HRM_Project.DTOs.Response;
    using global::HRM_Project.Exceptions;
    using global::HRM_Project.Models.DB;
    using global::HRM_Project.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PositionService(ApplicationDbContext context, IMapper mapper) : IPositionService
    {
        public IQueryable<Position> Search(string title = "", int page = 1, int size = 10)
        {
            return context.Positions.Include(p => p.Company)
                .Include(p => p.Department)
                .Include(p => p.Division)
                .Where(x => !x.IsDeleted && (string.IsNullOrEmpty(title) || x.Title.Contains(title.ToLower())))
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<PositionViewDto>> GetAllPositions()
        {
            return mapper.Map<List<PositionViewDto>>(
                await context.Positions.Where(p => !p.IsDeleted)
                    .Include(p => p.Company)
                    .Include(p => p.Department)
                    .Include(p => p.Division)
                    .ToListAsync()
            );
        }

        public async Task<PositionViewDto> GetByIdAsync(int id)
        {
            var entity = await context.Positions
                .Include(p => p.Company)
                .Include(p => p.Department)
                .Include(p => p.Division)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (entity == null) throw new ToException(ToErrors.POSITION_WITH_THIS_ID_NOT_FOUND);
            return mapper.Map<PositionViewDto>(entity);
        }

        public async Task<PositionViewDto> AddAsync(PositionCreateDto create)
        {
            if (!context.Companies.Any(c => c.Id == create.CompanyId && !c.IsDeleted))
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

            if (!context.Departments.Any(d => d.Id == create.DepartmentId && !d.IsDeleted))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (!context.Divisions.Any(d => d.Id == create.DivisionId && !d.IsDeleted))
                throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

            if (context.Positions.Any(p => !p.IsDeleted && p.Title == create.Title))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            Position position = mapper.Map<Position>(create);
            await context.AddAsync(position);
            await context.SaveChangesAsync();
            await context.Entry(position).Reference(p => p.Company).LoadAsync();
            await context.Entry(position).Reference(p => p.Department).LoadAsync();
            await context.Entry(position).Reference(p => p.Division).LoadAsync();
            return mapper.Map<PositionViewDto>(position);
        }

        public async Task<PositionViewDto> UpdateAsync(PositionUpdateDto update)
        {
            var res = await context.Positions.AsNoTracking()
                .FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == update.Id);

            if (res == null) throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);

            Position position = mapper.Map<Position>(update);
            context.Attach(position).State = EntityState.Modified;
            await context.SaveChangesAsync();
            await context.Entry(position).Reference(p => p.Company).LoadAsync();
            await context.Entry(position).Reference(p => p.Department).LoadAsync();
            await context.Entry(position).Reference(p => p.Division).LoadAsync();
            return mapper.Map<PositionViewDto>(position);
        }

        public async Task<PositionViewDto> DeleteAsync(int id)
        {
            var entity = await context.Positions
                .Include(p => p.Company)
                .Include(p => p.Department)
                .Include(p => p.Division)
                .FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

            if (entity == null)
                throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_DELETE);

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<PositionViewDto>(entity);
        }
    }

}
