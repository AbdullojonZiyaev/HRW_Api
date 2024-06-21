using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;

namespace HRM_Project.Services
{
    public interface ICompanyService
    {
        IQueryable<Company> Search ( string fullname = "", int page = 1, int size = 10 );
        Task<List<CompanyViewDto>> GetCompanies ();
        Task<CompanyViewDto> GetByIdAsync ( int Id );
        Task<CompanyViewDto> AddAsync ( CompanyCreateDto create );
        Task<CompanyViewDto> UpdateAsync ( CompanyUpdateDto update );
        Task<CompanyViewDto> DeleteAsync ( int Id );
    }
}
