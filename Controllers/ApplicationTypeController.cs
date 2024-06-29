using HRM_Project.DTOs.Request;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationTypeController(IApplicationTypeService applicationTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await applicationTypeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await applicationTypeService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ApplicationTypeCreateDto createDto)
        {
            var result = await applicationTypeService.AddAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ApplicationTypeUpdateDto updateDto)
        {
            var result = await applicationTypeService.UpdateAsync(updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await applicationTypeService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name, int page = 1, int size = 10)
        {
            var result = await applicationTypeService.Search(name, page, size).ToListAsync();
            return Ok(result);
        }
    }
}