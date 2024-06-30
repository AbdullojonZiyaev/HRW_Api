using HRM_Project.DTOs.Request;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSheetTypeController(ITimeSheetTypeService timeSheetTypeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await timeSheetTypeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await timeSheetTypeService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TimeSheetTypeCreateDto createDto)
        {
            var result = await timeSheetTypeService.AddAsync(createDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TimeSheetTypeUpdateDto updateDto)
        {
            var result = await timeSheetTypeService.UpdateAsync(updateDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await timeSheetTypeService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name, int page = 1, int size = 10)
        {
            var result = await timeSheetTypeService.Search(name, page, size).ToListAsync();
            return Ok(result);
        }
    }
}