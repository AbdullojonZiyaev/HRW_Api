using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using HRM_Project.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentController(IDepartmentService departmentService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string fullname, int page = 1, int size = 10)
        {
            var totalCount = await departmentService.Search(fullname, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<DepartmentViewDto>>(await departmentService.Search(fullname, page, size).ToListAsync());
            var pageData = new
            {
                TotalCount = totalCount,
                Page = page,
                Size = size,
                Items = result
            };
            return Ok(pageData);
        }

        [HttpGet("AllDepartments")]
        public async Task<ActionResult> GetAllDepartments() => Ok(await departmentService.GetDepartments());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) => Ok(await departmentService.GetByIdAsync(id));

        [HttpPost("AddDepartment")]
        public async Task<ActionResult> Post([FromBody] DepartmentCreateDto entityDto) => Ok(await departmentService.AddAsync(entityDto));

        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> Put([FromBody] DepartmentUpdateDto entityDto) => Ok(await departmentService.UpdateAsync(entityDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await departmentService.DeleteAsync(id));

        [HttpGet("{departmentId}/minimal-divisions")]
        public async Task<IActionResult> GetMinimalDivisionsByDepartmentId(int departmentId)
        {
            var divisions = await departmentService.GetMinimalDivisionsByDepartmentIdAsync(departmentId);
            return Ok(divisions);
        }

        [HttpGet("{departmentId}/minimal-employees")]
        public async Task<IActionResult> GetMinimalEmployeesByDepartmentId(int departmentId)
        {
            var employees = await departmentService.GetMinimalEmployeesByDepartmentIdAsync(departmentId);
            return Ok(employees);
        }

        [HttpGet("{departmentId}/minimal-vacancies")]
        public async Task<IActionResult> GetMinimalVacanciesByDepartmentId(int departmentId)
        {
            var vacancies = await departmentService.GetMinimalVacanciesByDepartmentIdAsync(departmentId);
            return Ok(vacancies);
        }
    }
}
