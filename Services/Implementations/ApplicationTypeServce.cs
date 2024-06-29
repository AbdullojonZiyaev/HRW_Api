using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.DB;
using HRM_Project.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class ApplicationTypeService(ApplicationDbContext context, IMapper mapper) : IApplicationTypeService
    {
        public async Task<ApplicationTypeViewDto> AddAsync(ApplicationTypeCreateDto createDto)
        {
            var applicationType = mapper.Map<ApplicationType>(createDto);
            await context.ApplicationTypes.AddAsync(applicationType);
            await context.SaveChangesAsync();
            return mapper.Map<ApplicationTypeViewDto>(applicationType);
        }

        public async Task<ApplicationTypeViewDto> DeleteAsync(int id)
        {
            var applicationType = await context.ApplicationTypes.FirstOrDefaultAsync(at => at.Id == id) ?? throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND);
            context.ApplicationTypes.Remove(applicationType);
            await context.SaveChangesAsync();
            return mapper.Map<ApplicationTypeViewDto>(applicationType);
        }

        public async Task<List<ApplicationTypeViewDto>> GetAllAsync()
        {
            var applicationTypes = await context.ApplicationTypes.ToListAsync();
            return mapper.Map<List<ApplicationTypeViewDto>>(applicationTypes);
        }

        public async Task<ApplicationTypeViewDto> GetByIdAsync(int id)
        {
            var applicationType = await context.ApplicationTypes.FirstOrDefaultAsync(at => at.Id == id);
            return applicationType == null
                ? throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND)
                : mapper.Map<ApplicationTypeViewDto>(applicationType);
        }

        public async Task<ApplicationTypeViewDto> UpdateAsync(ApplicationTypeUpdateDto updateDto)
        {
            var applicationType = await context.ApplicationTypes.FirstOrDefaultAsync(at => at.Id == updateDto.Id) ?? throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);
            mapper.Map(updateDto, applicationType);
            await context.SaveChangesAsync();
            return mapper.Map<ApplicationTypeViewDto>(applicationType);
        }

        public IQueryable<ApplicationType> Search(string name = "", int page = 1, int size = 10)
        {
            return context.ApplicationTypes
                .Where(at => !at.IsDeleted && (string.IsNullOrEmpty(name) || at.Name.Contains(name)))
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }

}
