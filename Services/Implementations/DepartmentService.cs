﻿using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DepartmentService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IQueryable<Department> Search(string fullname = "", int page = 1, int size = 10)
        {
            return context.Departments
                .Include(d => d.Company)
                .Where(d => !d.IsDeleted && (string.IsNullOrEmpty(fullname) || d.Fullname.Contains(fullname.ToLower())))
                .OrderByDescending(d => d.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<DepartmentViewDto>> GetDepartments()
        {
            var departments = await context.Departments
                .Where(d => !d.IsDeleted)
                .Include(d => d.Company)
                .ToListAsync();

            return mapper.Map<List<DepartmentViewDto>>(departments);
        }

        public async Task<DepartmentViewDto> GetByIdAsync(int id)
        {
            var department = await context.Departments
                .Include(d => d.Company)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

            if (department == null)
            {
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);
            }

            return mapper.Map<DepartmentViewDto>(department);
        }

        public async Task<DepartmentViewDto> AddAsync(DepartmentCreateDto create)
        {
            if (!await context.Companies.AnyAsync(c => c.Id == create.CompanyId && !c.IsDeleted))
            {
                throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);
            }

            if (await context.Departments.AnyAsync(d => !d.IsDeleted && d.Fullname == create.Fullname))
            {
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);
            }

            var department = mapper.Map<Department>(create);
            await context.Departments.AddAsync(department);
            await context.SaveChangesAsync();

            await context.Entry(department).Reference(d => d.Company).LoadAsync();

            return mapper.Map<DepartmentViewDto>(department);
        }

        public async Task<DepartmentViewDto> UpdateAsync(DepartmentUpdateDto update)
        {
            var existingDepartment = await context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == update.Id && !d.IsDeleted);

            if (existingDepartment == null)
            {
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);
            }

            var department = mapper.Map<Department>(update);
            context.Attach(department).State = EntityState.Modified;
            await context.SaveChangesAsync();

            await context.Entry(department).Reference(d => d.Company).LoadAsync();

            return mapper.Map<DepartmentViewDto>(department);
        }

        public async Task<DepartmentViewDto> DeleteAsync(int id)
        {
            var department = await context.Departments
                .Include(d => d.Company)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

            if (department == null)
            {
                throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND_FOR_DELETE);
            }

            if (await context.Divisions.AnyAsync(d => d.DepartmentId == department.Id && !d.IsDeleted))
            {
                throw new ToException(ToErrors.DEPARTMENT_HAS_REFERENCES_DIVISIONS);
            }

            department.IsDeleted = true;
            await context.SaveChangesAsync();

            return mapper.Map<DepartmentViewDto>(department);
        }

        public async Task<List<MinimalDivisionViewDto>> GetMinimalDivisionsByDepartmentIdAsync(int departmentId)
        {
            var divisions = await context.Divisions
                .Where(d => d.DepartmentId == departmentId && !d.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<MinimalDivisionViewDto>>(divisions);
        }

        public async Task<List<MinimalEmployeeViewDto>> GetMinimalEmployeesByDepartmentId(int departmentId)
        {
            var minimalEmployees = await context.Employees
                .Where(e => !e.IsDeleted && e.DepartmentId == departmentId)
                .Include(e => e.Position) // Include the Position entity
                .Select(e => new MinimalEmployeeViewDto
                {
                    Id = e.Id,
                    Fullname = e.FirstName + " " + e.SurName,
                    PositionTitle = e.Position.Title // Now you can access Title because Position is included
                })
                .ToListAsync();

            return minimalEmployees;
        }


        public async Task<List<MinimalVacancyViewDto>> GetMinimalVacanciesByDepartmentIdAsync(int departmentId)
        {
            var vacancies = await context.Vacancies
                .Where(v => v.DepartmentId == departmentId && !v.IsDeleted)
                .ToListAsync();

            return mapper.Map<List<MinimalVacancyViewDto>>(vacancies);
        }
    }
}
