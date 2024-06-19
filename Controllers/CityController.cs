using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IMapper mapper;

        public CityController ( ICityService cityService, IMapper mapper )
        {
            this.cityService = cityService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get ( string name, int page = 1, int size = 10 )
        {
            var TotalCount = await cityService.Search (name, 1, int.MaxValue).CountAsync ();
            var result = mapper.Map<List<CityViewDto>> (await cityService.Search (name, page, size).ToListAsync ());
            var pageData = new
            {
                TotalCount,
                Page = page,
                Size = size,
                Items = result
            };
            return Ok (pageData);
        }
        [HttpGet ("AllCity")]
        public async Task<IActionResult> Get ()  => Ok (await cityService.GetCities ());

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetById ( int id ) => Ok (await cityService.GetByIdAsync (id));

        [HttpPost ("AddCity")]
        public async Task<IActionResult> Post ( [FromBody] CityCreateDto entityDto ) => Ok (await cityService.AddAsync (entityDto));

        [HttpPut ("UpdateCity")]
        public async Task<IActionResult> Put ( [FromBody] CityUpdateDto entityDto ) => Ok (await cityService.UpdateAsync (entityDto));

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete ( int id ) => Ok (await cityService.DeleteAsync (id));
    }
}
