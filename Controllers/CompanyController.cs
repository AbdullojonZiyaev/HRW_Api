using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using HRM_Project.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM_Project.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    //[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyController(ICompanyService companyService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get ( string fullname, int page = 1, int size = 10 )
        {
            var TotalCount = await companyService.Search (fullname, 1, int.MaxValue).CountAsync ();
            var result = mapper.Map<List<CompanyViewDto>> (await companyService.Search (fullname, page, size).ToListAsync ());
            var pageData = new
            {
                TotalCount,
                Page = page,
                Size = size,
                Items = result
            };
            return Ok (pageData);
        }

        [HttpGet ("AllCompanies")]
        public async Task<ActionResult> Get () => Ok (await companyService.GetCompanies ());
        
        [HttpGet ("{id}")]
        public async Task<ActionResult> GetById ( int id ) => Ok (await companyService.GetByIdAsync (id));
        
        [HttpPost ("AddCompany")]
        public async Task<ActionResult> Post ( [FromBody] CompanyCreateDto entityDto )=> Ok (await companyService.AddAsync (entityDto));

        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> Put([FromBody] CompanyUpdateDto entityDto) => Ok(await companyService.UpdateAsync(entityDto));
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await companyService.DeleteAsync(id));

    }
}
