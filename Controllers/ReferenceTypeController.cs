using HRM_Project.DTOs.Request;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceTypeController(IReferenceTypeService referenceTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await referenceTypeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await referenceTypeService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReferenceTypeCreateDto createDto)
        {
            var result = await referenceTypeService.AddAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReferenceTypeUpdateDto updateDto)
        {
            var result = await referenceTypeService.UpdateAsync(updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await referenceTypeService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name, int page = 1, int size = 10)
        {
            var result = await referenceTypeService.Search(name, page, size).ToListAsync();
            return Ok(result);
        }
    }
}