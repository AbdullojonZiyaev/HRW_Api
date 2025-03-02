﻿using AuthKeeper.DTOs.Response;
using AutoMapper;
using HRM_Project.DTOs;
using HRM_Project.DTOs.Params;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.Common;
using HRM_Project.Models.DB;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Services.Implementations
{
    public class RoleService(IMapper mapper, ApplicationDbContext context) : IRoleService
    {
        public async Task<PagedList<Role, RoleViewDto>> SearchAsync(NameAndPagedParam param)
        {
            var query = context.Roles
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.Name) || x.Name.Contains(param.Name) ))
                .OrderBy(x => x.Id).AsQueryable();

            var count = await query.CountAsync();
            var roles = await query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<Role, RoleViewDto>(roles, count, param.Page, param.Size, mapper);
        }
        public async Task<RoleViewDto> AddAsync(RoleCreateDto create)
        {
            if (await context.Roles.AnyAsync(c => !c.IsDeleted && c.Name == create.Name))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            var role = mapper.Map<Role>(create);
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();
            return mapper.Map<RoleViewDto>(role);
        }
        public async Task<RoleViewDto> UpdateAsync(RoleUpdateDto update)
        {
            var role = await context.Roles.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == update.Id );
            if (role is null)
                throw new ToException(ToErrors.ROLE_NOT_FOUND);

            if (await context.Roles.AnyAsync(c => !c.IsDeleted && c.Id != update.Id && c.Name == update.Name))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            role.Name = update.Name;
            role.Functionals = update.Functionals;
            await context.SaveChangesAsync();
            return mapper.Map<RoleViewDto>(role);
        }
        public async Task<RoleViewDto> DeleteAsync(int id)
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
            if (role is null)
                throw new ToException(ToErrors.ROLE_NOT_FOUND);

            if (await context.Users.AnyAsync(x => !x.IsDeleted && x.RoleId == id))
                throw new ToException(ToErrors.ENTITY_ALREADY_USED);

            role.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<RoleViewDto>(role);
        }
        public IList<string> GetAllFunctionals()
            => Functional.GetAllFunctionals();
    }
}
