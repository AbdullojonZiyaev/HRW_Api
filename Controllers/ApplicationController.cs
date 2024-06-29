using HRM_Project.DTOs.Request;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController(IApplicationService applicationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await applicationService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await applicationService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ApplicationCreateDto createDto)
        {
            var result = await applicationService.AddAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ApplicationUpdateDto updateDto)
        {
            var result = await applicationService.UpdateAsync(updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await applicationService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string description, int page = 1, int size = 10)
        {
            var result = await applicationService.Search(description, page, size).ToListAsync();
            return Ok(result);
        }
    }
}