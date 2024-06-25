using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.DB;
using HRM_Project.Models.Types;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class OrderTypeService(ApplicationDbContext context, IMapper mapper) : IOrderTypeService
    {
        public IQueryable<OrderType> Search(string name = "", string category = "", int page = 1, int size = 10)
        {
            return context.OrderTypes
                .Where(ot => !ot.IsDeleted &&
                             (string.IsNullOrEmpty(name) || ot.Name.Contains(name)) &&
                             (string.IsNullOrEmpty(category) || ot.OrderCategory.Contains(category)))
                .OrderByDescending(ot => ot.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<OrderTypeViewDto>> GetOrderTypes()
        {
            return mapper.Map<List<OrderTypeViewDto>>(
                await context.OrderTypes.Where(ot => !ot.IsDeleted).ToListAsync()
            );
        }

        public async Task<OrderTypeViewDto> GetByIdAsync(int id)
        {
            var orderType = await context.OrderTypes.FirstOrDefaultAsync(ot => ot.Id == id && !ot.IsDeleted);
            if (orderType == null) throw new ToException(ToErrors.ORDERTYPE_WITH_THIS_ID_NOT_FOUND);
            return mapper.Map<OrderTypeViewDto>(orderType);
        }

        public async Task<OrderTypeViewDto> AddAsync(OrderTypeCreateDto createDto)
        {
            if (context.OrderTypes.Any(ot => ot.Name == createDto.Name && !ot.IsDeleted))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            var orderType = mapper.Map<OrderType>(createDto);
            await context.OrderTypes.AddAsync(orderType);
            await context.SaveChangesAsync();
            return mapper.Map<OrderTypeViewDto>(orderType);
        }

        public async Task<OrderTypeViewDto> DeleteAsync(int id)
        {
            var orderType = await context.OrderTypes.FirstOrDefaultAsync(ot => ot.Id == id && !ot.IsDeleted);
            if (orderType == null) throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND_FOR_DELETE);

            orderType.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<OrderTypeViewDto>(orderType);
        }
    }
}