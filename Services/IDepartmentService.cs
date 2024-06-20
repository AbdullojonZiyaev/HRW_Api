﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models.Common;

namespace HRM_Project.Services
{
    public interface IDepartmentService
    {
        IQueryable<Department> Search(string fullname = "", int page = 1, int size = 10);
        Task<List<DepartmentViewDto>> GetDepartments();
        Task<DepartmentViewDto> GetByIdAsync(int id);
        Task<DepartmentViewDto> AddAsync(DepartmentCreateDto create);
        Task<DepartmentViewDto> UpdateAsync(DepartmentUpdateDto update);
        Task<DepartmentViewDto> DeleteAsync(int id);
    }
}
