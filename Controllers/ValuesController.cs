using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class VacancyController(IVacancyService vacancyService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await vacancyService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok( await vacancyService.GetByIdAsync(id));
    

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] VacancyCreateDto createDto) => Ok(await vacancyService.AddAsync(createDto));

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] VacancyUpdateDto updateDto) => Ok(await vacancyService.UpdateAsync(updateDto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => Ok(await vacancyService.DeleteAsync(id));

    [HttpGet("search")]
    public async Task<IActionResult> Search(string title, int page = 1, int size = 10)
    {
        var totalCount = await vacancyService.Search(title, 1, int.MaxValue).CountAsync();
        var result = mapper.Map<List<VacancyViewDto>>(await vacancyService.Search(title, page, size).ToListAsync());
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
