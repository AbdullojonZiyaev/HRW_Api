using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services
{
    public interface IActService
    {
        IQueryable<Act> Search(string actNumber = "", string description = "", int page = 1, int size = 10);
        Task<List<ActViewDto>> GetActs();
        Task<ActViewDto> GetByIdAsync(int id);
        Task<ActViewDto> AddAsync(ActCreateDto createDto);
        Task<ActViewDto> UpdateAsync(ActUpdateDto updateDto);
        Task<ActViewDto> DeleteAsync(int id);
    }
}