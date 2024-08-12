using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using HRM_Project.Services.HRM_Project.Services;
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
    public class DivisionController(IDivisionService divisionService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string fullname, int page = 1, int size = 10)
        {
            var totalCount = await divisionService.Search(fullname, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<DivisionViewDto>>(await divisionService.Search(fullname, page, size).ToListAsync());
            var pageData = new
            {
                TotalCount = totalCount,
                Page = page,
                Size = size,
                Items = result
            };
            return Ok(pageData);
        }

        [HttpGet("AllDivisions")]
        public async Task<ActionResult> GetAllDivisions() => Ok(await divisionService.GetDivisions());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) => Ok(await divisionService.GetByIdAsync(id));

        [HttpPost("AddDivision")]
        public async Task<ActionResult> Post([FromBody] DivisionCreateDto entityDto) => Ok(await divisionService.AddAsync(entityDto));

        [HttpPut("UpdateDivision")]
        public async Task<IActionResult> Put([FromBody] DivisionUpdateDto entityDto) => Ok(await divisionService.UpdateAsync(entityDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await divisionService.DeleteAsync(id));

        [HttpGet("MinimalEmployees/{divisionId}")]
        public async Task<IActionResult> GetMinimalEmployees(int divisionId) => Ok(await divisionService.GetMinimalEmployeesByDivisionId(divisionId));
    }
}
