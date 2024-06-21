using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HRM_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;
        private readonly IMapper _mapper;

        public PositionController(IPositionService positionService, IMapper mapper)
        {
            _positionService = positionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string title, int page = 1, int size = 10)
        {
            var totalCount = await _positionService.Search(title, 1, int.MaxValue).CountAsync();
            var result = _mapper.Map<List<PositionViewDto>>(await _positionService.Search(title, page, size).ToListAsync());
            var pageData = new
            {
                TotalCount = totalCount,
                Page = page,
                Size = size,
                Items = result
            };
            return Ok(pageData);
        }

        [HttpGet("AllPositions")]
        public async Task<ActionResult> Get() => Ok(await _positionService.GetAllPositions());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id) => Ok(await _positionService.GetByIdAsync(id));

        [HttpPost("AddPosition")]
        public async Task<ActionResult> Post([FromBody] PositionCreateDto entityDto) => Ok(await _positionService.AddAsync(entityDto));

        [HttpPut("UpdatePosition")]
        public async Task<IActionResult> Put([FromBody] PositionUpdateDto entityDto) => Ok(await _positionService.UpdateAsync(entityDto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _positionService.DeleteAsync(id));
    }
}
