using HRM_Project.DTOs;
using HRM_Project.DTOs.Params;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Common;

namespace HRM_Project.Services
{
    public interface IUserService
    {
        Task<PagedList<User, UserViewDto>> SearchAsync(NameAndPagedParam param);
        Task<UserViewDto> AddAsync(UserCreateDto create);
        Task<UserViewDto> UpdateAsync(UserUpdateDto update);
        Task<UserViewDto> DeleteAsync(int id);
        Task<UserInfoDto> GetUserInfoAsync();
        Task<User> GetCurrentAsync();
        Task<UserViewDto> ChangePasswordAsync(ChangePasswordDto changeObj);
        Task<UserViewDto> GetByUsernameAsync ( string username );
    }
}
