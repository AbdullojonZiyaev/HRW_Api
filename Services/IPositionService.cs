namespace HRM_Project.Services
{
    using global::HRM_Project.DTOs.Request;
    using global::HRM_Project.DTOs.Response;
    using global::HRM_Project.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IPositionService
    {
        IQueryable<Position> Search(string title = "", int page = 1, int size = 10);

        Task<List<PositionViewDto>> GetAllPositions();

        Task<PositionViewDto> GetByIdAsync(int id);

        Task<PositionViewDto> AddAsync(PositionCreateDto create);

        Task<PositionViewDto> UpdateAsync(PositionUpdateDto update);

        Task<PositionViewDto> DeleteAsync(int id);
        Task<List<MinimalEmployeeViewDto>> GetMinimalEmployeesByPositionId(int positionId);
    }

}
