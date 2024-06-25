using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;

namespace HRM_Project.Services
{
    public interface IOrderService
    {
        Task<OrderViewDto> AddAsync(OrderCreateDto createDto);
        Task<OrderViewDto> DeleteAsync(int id);
        Task<OrderViewDto> GetByIdAsync(int id);
        Task<List<OrderViewDto>> GetAllAsync();
        IQueryable<Order> Search(string orderNumber, int page = 1, int size = 10);
    }

}
