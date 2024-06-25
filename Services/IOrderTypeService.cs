using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Types;

public interface IOrderTypeService
{
    IQueryable<OrderType> Search(string name = "", string category = "", int page = 1, int size = 10);
    Task<List<OrderTypeViewDto>> GetOrderTypes();
    Task<OrderTypeViewDto> GetByIdAsync(int id);
    Task<OrderTypeViewDto> AddAsync(OrderTypeCreateDto createDto);
    Task<OrderTypeViewDto> DeleteAsync(int id);
}
