using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using HRM_Project.Models.Types;
using HRM_Project.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services.Implementations
{
    public class ReferenceTypeService(ApplicationDbContext context, IMapper mapper) : IReferenceTypeService
    {
        public async Task<List<ReferenceTypeViewDto>> GetAllAsync()
        {
            var referenceTypes = await context.ReferenceTypes
                .Where(rt => !rt.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<ReferenceTypeViewDto>>(referenceTypes);
        }

        public async Task<ReferenceTypeViewDto> GetByIdAsync(int id)
        {
            var referenceType = await context.ReferenceTypes
                .FirstOrDefaultAsync(rt => rt.Id == id && !rt.IsDeleted);

            return referenceType == null
                ? throw new ToException(ToErrors.REFERENCETYPE_WITH_THIS_ID_NOT_FOUND)
                : mapper.Map<ReferenceTypeViewDto>(referenceType);
        }

        public async Task<ReferenceTypeViewDto> AddAsync(ReferenceTypeCreateDto createDto)
        {
            var referenceType = mapper.Map<ReferenceType>(createDto);
            await context.ReferenceTypes.AddAsync(referenceType);
            await context.SaveChangesAsync();
            return mapper.Map<ReferenceTypeViewDto>(referenceType);
        }

        public async Task<ReferenceTypeViewDto> UpdateAsync(ReferenceTypeUpdateDto updateDto)
        {
            var referenceType = await context.ReferenceTypes.FindAsync(updateDto.Id);

            if (referenceType == null || referenceType.IsDeleted)
                throw new ToException(ToErrors.REFERENCETYPE_WITH_THIS_ID_NOT_FOUND);

            mapper.Map(updateDto, referenceType);
            context.ReferenceTypes.Update(referenceType);
            await context.SaveChangesAsync();
            return mapper.Map<ReferenceTypeViewDto>(referenceType);
        }

        public async Task<ReferenceTypeViewDto> DeleteAsync(int id)
        {
            var referenceType = await context.ReferenceTypes.FindAsync(id);

            if (referenceType == null || referenceType.IsDeleted)
                throw new ToException(ToErrors.REFERENCETYPE_WITH_THIS_ID_NOT_FOUND);

            referenceType.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<ReferenceTypeViewDto>(referenceType);
        }

        public IQueryable<ReferenceType> Search(string name = "", int page = 1, int size = 10)
        {
            return context.ReferenceTypes
                .Where(rt => !rt.IsDeleted && (string.IsNullOrEmpty(name) || rt.Name.Contains(name)))
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }
}