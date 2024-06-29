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
    public class OrderController(IOrderService orderService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await orderService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await orderService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderCreateDto createDto) => Ok(await orderService.AddAsync(createDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await orderService.DeleteAsync(id));

        [HttpGet("Search")]
        public async Task<IActionResult> Search(string orderNumber = "", int page = 1, int size = 10)
        {
            var totalCount = await orderService.Search(orderNumber, 1, int.MaxValue).CountAsync();
            var result = mapper.Map<List<OrderViewDto>>(await orderService.Search(orderNumber, page, size).ToListAsync());
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
