using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActTypeController(IActTypeService actTypeService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
         Ok(await actTypeService.GetActTypes());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) =>
            Ok(await actTypeService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ActTypeCreateDto createDto) =>
            Ok(await actTypeService.AddAsync(createDto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ActTypeUpdateDto updateDto) =>
            Ok(await actTypeService.UpdateAsync(updateDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await actTypeService.DeleteAsync(id));

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name = "", string description = "", int page = 1, int size = 10)
        {
            var totalCount = await actTypeService.Search(name, description, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<ActTypeViewDto>>(await actTypeService.Search(name, description, page, size).ToListAsync());
            var pageData = new
            {
                TotalCount = totalCount,
                Page = page,
                Size = size,
                Items = result
            };
            return Ok(pageData);
        }
    }
}
