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
    public class NewsController(INewsService newsService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await newsService.GetNews());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await newsService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NewsCreateDto createDto) => Ok(await newsService.AddAsync(createDto));

        [HttpPut("UpdateNews")]
        public async Task<IActionResult> Update([FromBody] NewsUpdateDto updateDto) => Ok(await newsService.UpdateAsync(updateDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await newsService.DeleteAsync(id));

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string title = "", string content = "", string tags = "", int page = 1, int size = 10)
        {
            var totalCount = await newsService.Search(title, content, tags, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<NewsViewDto>>(await newsService.Search(title, content, tags, page, size).ToListAsync());
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
