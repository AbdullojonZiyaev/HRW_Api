using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Types;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services
{
    public interface IActTypeService
    {
        IQueryable<ActType> Search(string name = "", string description = "", int page = 1, int size = 10);
        Task<List<ActTypeViewDto>> GetActTypes();
        Task<ActTypeViewDto> GetByIdAsync(int id);
        Task<ActTypeViewDto> AddAsync(ActTypeCreateDto createDto);
        Task<ActTypeViewDto> UpdateAsync(ActTypeUpdateDto updateDto);
        Task<ActTypeViewDto> DeleteAsync(int id);
    }
}