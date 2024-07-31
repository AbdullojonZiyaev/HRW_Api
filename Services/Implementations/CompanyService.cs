using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace HRM_Project.Services.Implementations
{
    public class CompanyService(ApplicationDbContext context, IMapper mapper) : ICompanyService
    {
        public IQueryable<Company> Search(string fullname = "", int page = 1, int size = 10)
        {
            return context.Companies.Include(c => c.City).Where(x => !x.IsDeleted && (string.IsNullOrEmpty(fullname) || x.Fullname.Contains(fullname.ToLower())))
             .OrderByDescending(x => x.Id)
             .Skip((page - 1) * size)
                 .Take(size)
                 .AsQueryable()
                 .OrderBy(x => x.Id);
        }
        public async Task<List<CompanyViewDto>> GetCompanies()
        {
            return mapper.Map<List<CompanyViewDto>>
                (await context.Companies.Where(c => !c.IsDeleted)
                .Include(a => a.City).ToListAsync());
        }

        public async Task<CompanyViewDto> AddAsync(CompanyCreateDto create)
        {
            if (!context.Cities.Any(c => c.Id == create.CityId && !c.IsDeleted))
                throw new ToException(ToErrors.CITY_WITH_THIS_ID_NOT_FOUND);
            if (context.Companies.Any(a => !a.IsDeleted && a.Fullname == create.Fullname))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);
            Company comp = mapper.Map<Company>(create);
            await context.AddAsync(comp);
            await context.SaveChangesAsync();
            await context.Entry(comp).Reference(a => a.City).LoadAsync();
            return mapper.Map<CompanyViewDto>(comp);
        }
        public async Task<CompanyViewDto> GetByIdAsync(int Id)
        {
            var entity = await context.Companies.Include(x => x.City).FirstOrDefaultAsync(x => x.Id == Id && !x.IsDeleted);
            if (entity == null) throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);
            return mapper.Map<CompanyViewDto>(entity);
        }

        public async Task<CompanyViewDto> UpdateAsync(CompanyUpdateDto update)
        {
            var res = await context.Companies.AsNoTracking().FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == update.Id);
            if (res == null) throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);

            Company comp = mapper.Map<Company>(update);
            context.Attach(comp).State = EntityState.Modified;
            await context.SaveChangesAsync();
            await context.Entry(comp).Reference(a => a.City).LoadAsync();
            return mapper.Map<CompanyViewDto>(comp);
        }
        public async Task<CompanyViewDto> DeleteAsync(int Id)
        {
            var entity = await context.Companies
                 .Include(s => s.City)
                 .FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == Id);
            if (entity == null)
                throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_DELETE);
            if (await context.Departments.AnyAsync(s => s.CompanyId == entity.Id && !s.IsDeleted)) throw new ToException
                    (ToErrors.COMPANY_HAS_REFFERENCE_DEPARTMENTS);
            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<CompanyViewDto>(entity);
        }
    }
}
