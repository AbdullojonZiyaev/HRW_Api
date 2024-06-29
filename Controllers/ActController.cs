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
    public class ActController(IActService actService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await actService.GetActs());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await actService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ActCreateDto createDto) => Ok(await actService.AddAsync(createDto));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ActUpdateDto updateDto) => Ok(await actService.UpdateAsync(updateDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await actService.DeleteAsync(id));

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string actNumber = "", string description = "", int page = 1, int size = 10)
        {
            var totalCount = await actService.Search(actNumber, description, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<ActViewDto>>(await actService.Search(actNumber, description, page, size).ToListAsync());
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
