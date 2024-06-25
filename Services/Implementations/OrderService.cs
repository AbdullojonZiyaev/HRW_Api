using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class OrderService(ApplicationDbContext context, IMapper mapper) : IOrderService
    {
        public async Task<OrderViewDto> AddAsync(OrderCreateDto createDto)
        {
            if (!context.Employees.Any(e => e.Id == createDto.EmployeeId && !e.IsDeleted))
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND);

            if (!context.OrderTypes.Any(ot => ot.Id == createDto.OrderTypeId && !ot.IsDeleted))
                throw new ToException(ToErrors.ORDERTYPE_WITH_THIS_ID_NOT_FOUND);

            if (!context.Users.Any(u => u.Id == createDto.UserId && !u.IsDeleted))
                throw new ToException(ToErrors.USER_WITH_THIS_ID_NOT_FOUND);

            if (!context.Companies.Any(c => c.Id == createDto.CompanyId && !c.IsDeleted))
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

            if (!context.Departments.Any(d => d.Id == createDto.DepartmentId && !d.IsDeleted))
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

            if (createDto.DivisionId.HasValue && !context.Divisions.Any(d => d.Id == createDto.DivisionId && !d.IsDeleted))
                throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

            Order order = mapper.Map<Order>(createDto);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            await context.Entry(order).Reference(o => o.Employee).LoadAsync();
            await context.Entry(order).Reference(o => o.OrderType).LoadAsync();
            await context.Entry(order).Reference(o => o.User).LoadAsync();
            await context.Entry(order).Reference(o => o.Company).LoadAsync();
            await context.Entry(order).Reference(o => o.Department).LoadAsync();
            await context.Entry(order).Reference(o => o.Division).LoadAsync();
            return mapper.Map<OrderViewDto>(order);
        }

        public async Task<OrderViewDto> DeleteAsync(int id)
        {
            var entity = await context.Orders
                .Include(o => o.Employee)
                .Include(o => o.OrderType)
                .Include(o => o.User)
                .Include(o => o.Company)
                .Include(o => o.Department)
                .Include(o => o.Division)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

            if (entity == null)
                throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND);

            entity.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<OrderViewDto>(entity);
        }

        public async Task<OrderViewDto> GetByIdAsync(int id)
        {
            var entity = await context.Orders
                .Include(o => o.Employee)
                .Include(o => o.OrderType)
                .Include(o => o.User)
                .Include(o => o.Company)
                .Include(o => o.Department)
                .Include(o => o.Division)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

            if (entity == null)
                throw new ToException(ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND);

            return mapper.Map<OrderViewDto>(entity);
        }

        public async Task<List<OrderViewDto>> GetAllAsync()
        {
            var entities = await context.Orders
                .Include(o => o.Employee)
                .Include(o => o.OrderType)
                .Include(o => o.User)
                .Include(o => o.Company)
                .Include(o => o.Department)
                .Include(o => o.Division)
                .Where(o => !o.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<OrderViewDto>>(entities);
        }

        public IQueryable<Order> Search(string orderNumber = "", int page = 1, int size = 10)
        {
            return context.Orders.Include(o => o.Employee)
                .Include(o => o.OrderType)
                .Include(o => o.User)
                .Include(o => o.Company)
                .Include(o => o.Department)
                .Include(o => o.Division)
                .Where(o => !o.IsDeleted && (string.IsNullOrEmpty(orderNumber) || o.OrderNumber.Contains(orderNumber)))
                .OrderByDescending(o => o.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }
    }
}