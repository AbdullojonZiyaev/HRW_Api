using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.DB;
using HRM_Project.Models.Types;
using HRM_Project.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services.Implementations
{
    public class ActTypeService(ApplicationDbContext context, IMapper mapper) : IActTypeService
    {
        public IQueryable<ActType> Search(string name = "", string description = "", int page = 1, int size = 10)
        {
            return context.ActTypes
                .Where(at => !at.IsDeleted &&
                             (string.IsNullOrEmpty(name) || at.Name.Contains(name)) &&
                             (string.IsNullOrEmpty(description) || at.Description.Contains(description)))
                .OrderByDescending(at => at.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<ActTypeViewDto>> GetActTypes()
        {
            return mapper.Map<List<ActTypeViewDto>>(
                await context.ActTypes.Where(at => !at.IsDeleted).ToListAsync()
            );
        }

        public async Task<ActTypeViewDto> GetByIdAsync(int id)
        {
            var actType = await context.ActTypes.FirstOrDefaultAsync(at => at.Id == id && !at.IsDeleted);

            return actType == null ? throw new ToException(ToErrors.ACTTYPE_WITH_THIS_ID_NOT_FOUND) : mapper.Map<ActTypeViewDto>(actType);
        }

        public async Task<ActTypeViewDto> AddAsync(ActTypeCreateDto createDto)
        {
            var actType = mapper.Map<ActType>(createDto);
            await context.ActTypes.AddAsync(actType);
            await context.SaveChangesAsync();
            return mapper.Map<ActTypeViewDto>(actType);
        }

        public async Task<ActTypeViewDto> UpdateAsync(ActTypeUpdateDto updateDto)
        {
            var actType = await context.ActTypes.AsNoTracking().FirstOrDefaultAsync(at => at.Id == updateDto.Id && !at.IsDeleted) ?? throw new ToException(ToErrors.ACTTYPE_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);
            actType = mapper.Map<ActType>(updateDto);
            context.ActTypes.Update(actType);
            await context.SaveChangesAsync();
            return mapper.Map<ActTypeViewDto>(actType);
        }

        public async Task<ActTypeViewDto> DeleteAsync(int id)
        {
            var actType = await context.ActTypes.FirstOrDefaultAsync(at => at.Id == id && !at.IsDeleted) ?? throw new ToException(ToErrors.ACTTYPE_WITH_THIS_ID_NOT_FOUND);
            actType.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<ActTypeViewDto>(actType);
        }
    }
}