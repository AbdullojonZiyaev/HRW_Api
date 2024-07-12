using AutoMapper;
using BCrypt.Net;
using HRM_Project.DTOs;
using HRM_Project.DTOs.Params;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.Common;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class UserService(ApplicationDbContext context, IMapper mapper, IAccountService accountService) : IUserService
    {
        public async Task<PagedList<User, UserViewDto>> SearchAsync(NameAndPagedParam param)
        {
            Console.WriteLine($"SearchAsync called with Name: {param.Name}, Page: {param.Page}, Size: {param.Size}");

            var query = context.Users
                .Include(x => x.Role)
                .Include(x => x.Company)  // Include the Company entity
                .Where(x => !x.IsDeleted &&
                            (string.IsNullOrWhiteSpace(param.Name) || x.FirstName.Contains(param.Name)))
                .OrderBy(x => x.Id).AsQueryable();

            var count = await query.CountAsync();

            var users = await query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<User, UserViewDto>(users, count, param.Page, param.Size, mapper);
        }


        public async Task<UserViewDto> AddAsync(UserCreateDto create)
        {
            if (await context.Users.AnyAsync(u => !u.IsDeleted && u.Username == create.Username))
                throw new ToException(ToErrors.THIS_USERNAME_ALREADY_EXIST);

            if (!await context.Roles.AnyAsync(u => !u.IsDeleted && u.Id == create.RoleId))
                throw new ToException(ToErrors.ROLE_NOT_FOUND);

            User user = mapper.Map<User>(create);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await context.AddAsync(user);
            await context.SaveChangesAsync();
            await context.Entry(user).Reference(p => p.Role).LoadAsync();
            return mapper.Map<UserViewDto>(user);
        }
        public async Task<UserViewDto> UpdateAsync(UserUpdateDto update)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == update.Id && !u.IsDeleted);
            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            if (await context.Users.AnyAsync(u => !u.IsDeleted && u.Id != update.Id && u.Username == update.Username))
                throw new ToException(ToErrors.THIS_USERNAME_ALREADY_EXIST);

            user.FirstName = update.FirstName; 
            user.SecondName = update.SecondName; 
            user.Surname = update.Surname; 
            user.Username = update.Username;
            user.Password = string.IsNullOrWhiteSpace(update.Password) ? user.Password : BCrypt.Net.BCrypt.HashPassword(update.Password);
            user.Address = update.Address;
            user.Phone = update.Phone;
            user.Position = update.Position;
            user.CompanyId = update.CompanyId;
            user.RoleId = update.RoleId;
            user.IsActived = update.IsActived;
            await context.SaveChangesAsync();
            context.Entry(user).Reference(p => p.Role).Load();
            accountService.RevokeRefreshToken(user.Username);

            return mapper.Map<UserViewDto>(user);
        }
        public async Task<UserViewDto> DeleteAsync(int id)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            user.IsDeleted = true;
            await context.SaveChangesAsync();
            accountService.RevokeRefreshToken(user.Username);

            return mapper.Map<UserViewDto>(user);
        }
        public async Task<User> GetCurrentAsync()
        {

            var username = accountService.Username();
            var user = await context.Users
                 .AsNoTracking()
                 .Include(x => x.Role)
                 .FirstOrDefaultAsync(x => !x.IsDeleted && x.Username == username);

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);
            return user;
        }
        public async Task<UserInfoDto> GetUserInfoAsync()
        {
            var username = accountService.Username();
            var user = await context.Users
                .Include(x => x.Role)
                .Where(x => !x.IsDeleted && x.Username == username)
                .Select(x => mapper.Map<UserInfoDto>(x))
                .FirstOrDefaultAsync();

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);
            return user;
        }
        public async Task<UserViewDto> ChangePasswordAsync(ChangePasswordDto changeObj)
        {
            var username = accountService.Username();
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Username == username);

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            if (!BCrypt.Net.BCrypt.Verify(changeObj.OldPassword, user.Password))
                throw new ToException(ToErrors.INVALID_CREDENTIALS);

            user.Password = BCrypt.Net.BCrypt.HashPassword(changeObj.NewPassword);

            await context.SaveChangesAsync();
            context.Entry(user).Reference(p => p.Role).Load();
            return mapper.Map<UserViewDto>(user);
        }
        public async Task<UserViewDto> GetByUsernameAsync ( string username )
        {
            var user = await context.Users.Include (u => u.Company).FirstOrDefaultAsync (x => x.Username == username);
            if (user == null)
                throw new ToException (ToErrors.ENTITY_WITH_THIS_ID_NOT_FOUND);

            context.Entry (user).Reference (p => p.Role).Load ();
            context.Entry (user).Reference (p => p.Company).Load ();
            return mapper.Map<UserViewDto> (user);
        }



    }
}
