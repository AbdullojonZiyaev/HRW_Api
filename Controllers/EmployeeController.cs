using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Project.Controllers
{
    using AutoMapper;
    using global::HRM_Project.DTOs.Request;
    using global::HRM_Project.DTOs.Response;
    using global::HRM_Project.Services;
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
        public class EmployeeController(IEmployeeService employeeService, IMapper mapper) : ControllerBase
        {
            [HttpGet]
            public async Task<IActionResult> Search(string fullname, int page = 1, int size = 10)
            {
                var totalCount = await employeeService.Search(fullname, 1, int.MaxValue).CountAsync();
                var result = mapper.Map<List<EmployeeViewDto>>(await employeeService.Search(fullname, page, size).ToListAsync());
                var pageData = new
                {
                    TotalCount = totalCount,
                    Page = page,
                    Size = size,
                    Items = result
                };
                return Ok(pageData);
            }

            [HttpGet("AllEmployees")]
            public async Task<ActionResult> Get() => Ok(await employeeService.GetAllEmployees());

            [HttpGet("{id}")]
            public async Task<ActionResult> GetById(int id) => Ok(await employeeService.GetByIdAsync(id));

            [HttpPost("AddEmployee")]
            public async Task<ActionResult> Post([FromBody] EmployeeCreateDto entityDto) => Ok(await employeeService.AddAsync(entityDto));

            [HttpPut("UpdateEmployee")]
            public async Task<IActionResult> Put([FromBody] EmployeeUpdateDto entityDto) => Ok(await employeeService.UpdateAsync(entityDto));

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id) => Ok(await employeeService.DeleteAsync(id));
        }
    }

}
