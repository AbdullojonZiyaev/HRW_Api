using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.Common;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class CityService(ApplicationDbContext context, IMapper mapper) : ICityService
    {
        public IQueryable<City> Search ( string name = "", int page = 1, int size = 10 )
        {
            return context.Cities.Where (x => !x.IsDeleted && (string.IsNullOrEmpty (name) || x.Name.Contains (name.ToLower ())))
                 .OrderByDescending (x => x.Id)
                 .Skip ((page - 1) * size)
                 .Take (size)
                 .AsQueryable ();
        }
        public async Task<List<CityViewDto>> GetCities ()
        {
            return mapper.Map<List<CityViewDto>>
                (await context.Cities.Where (x => !x.IsDeleted).OrderByDescending (x => x.Id).ToListAsync ());
        }
        public async Task<CityViewDto> GetByIdAsync ( int Id )
        {
            var entity = await context.Cities.FirstOrDefaultAsync (c => c.Id == Id && !c.IsDeleted);
            if (entity == null)
                throw new ToException (ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND);
            return mapper.Map<CityViewDto> (entity);
        }
        public async Task<CityViewDto> AddAsync ( CityCreateDto city )
        {
            var existCity = await context.Cities.FirstOrDefaultAsync (c => c.Name == city.Name && !c.IsDeleted);
            if (existCity != null)
                throw new ToException (ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);
            City newCity = mapper.Map<City> (city);
            await context.AddAsync (newCity);
            await context.SaveChangesAsync ();
            return mapper.Map<CityViewDto> (newCity);
        }

        public async Task<CityViewDto> UpdateAsync ( CityUpdateDto city )
        {
            var eCity = await context.Cities.FirstOrDefaultAsync (c => !c.IsDeleted && c.Id == city.Id);
            if (eCity == null)
                throw new ToException (ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);
            eCity.Name = city.Name;
            context.Update (eCity);
            await context.SaveChangesAsync ();
            return mapper.Map<CityViewDto> (eCity);
        }
        public async Task<CityViewDto> DeleteAsync ( int Id )
        {
            var entity = await context.Cities.FirstOrDefaultAsync (c => !c.IsDeleted && c.Id == Id);
            if (entity == null)
                throw new ToException (ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_DELETE);
            if (await context.Companies.AnyAsync (s => s.CityId == entity.Id && !s.IsDeleted)) throw new ToException
                (ToErrors.CITY_HAS_REFFERENCE_TO_COMPANY);
            entity.IsDeleted = true;
            await context.SaveChangesAsync ();
            return mapper.Map<CityViewDto> (entity);
        }
    }
}
