using HRM_Project.DTOs.Request;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController(IReferenceService referenceService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await referenceService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await referenceService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReferenceCreateDto createDto)
        {
            var result = await referenceService.AddAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReferenceUpdateDto updateDto)
        {
            var result = await referenceService.UpdateAsync(updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await referenceService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string description, int page = 1, int size = 10)
        {
            var result = await referenceService.Search(description, page, size).ToListAsync();
            return Ok(result);
        }
    }
}