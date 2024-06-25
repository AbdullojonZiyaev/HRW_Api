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
    public class OrderTypeController(IOrderTypeService orderTypeService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await orderTypeService.GetOrderTypes());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await orderTypeService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderTypeCreateDto createDto) => Ok(await orderTypeService.AddAsync(createDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await orderTypeService.DeleteAsync(id));

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string name = "", string category = "", int page = 1, int size = 10)
        {
            var totalCount = await orderTypeService.Search(name, category, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<OrderTypeViewDto>>(await orderTypeService.Search(name, category, page, size).ToListAsync());
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
