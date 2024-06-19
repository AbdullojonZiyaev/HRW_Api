using AuthKeeper.DTOs.Response;
using HRM_Project.DTOs;
using HRM_Project.DTOs.Params;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Common;

namespace HRM_Project.Services
{
    public interface IRoleService
    {
        Task<PagedList<Role, RoleViewDto>> SearchAsync(NameAndPagedParam param);
        Task<RoleViewDto> AddAsync(RoleCreateDto create);
        Task<RoleViewDto> UpdateAsync(RoleUpdateDto update);
        Task<RoleViewDto> DeleteAsync(int id);
        IList<string> GetAllFunctionals();
    }
}
