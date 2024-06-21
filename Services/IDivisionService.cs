namespace HRM_Project.Services
{
    using global::HRM_Project.DTOs.Request;
    using global::HRM_Project.DTOs.Response;
    using global::HRM_Project.Models;
    using System.Linq;
    using System.Threading.Tasks;

    namespace HRM_Project.Services
    {
        public interface IDivisionService
        {
            IQueryable<Division> Search(string fullname = "", int page = 1, int size = 10);
            Task<List<DivisionViewDto>> GetDivisions();
            Task<DivisionViewDto> GetByIdAsync(int Id);
            Task<DivisionViewDto> AddAsync(DivisionCreateDto create);
            Task<DivisionViewDto> UpdateAsync(DivisionUpdateDto update);
            Task<DivisionViewDto> DeleteAsync(int Id);
        }
    }

}
