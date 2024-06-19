using AuthKeeper.DTOs.Response;
using HRM_Project.DTOs.Params;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs;
using HRM_Project.Models.Common;
using HRM_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HRM_Project.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        readonly IRoleService roleService;

        public RoleController ( IRoleService roleService )
            => this.roleService = roleService;

        [HttpGet]
        [ProducesResponseType (typeof (PagedList<Role, RoleViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync ( [FromQuery] NameAndPagedParam param )
           => Ok (await roleService.SearchAsync (param));

        [HttpPost]
        [ProducesResponseType (typeof (RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync ( [FromBody] RoleCreateDto create )
            => Ok (await roleService.AddAsync (create));

        [HttpPut]
        [ProducesResponseType (typeof (RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync ( [FromBody] RoleUpdateDto update )
            => Ok (await roleService.UpdateAsync (update));

        [HttpDelete ("{id}")]
        [ProducesResponseType (typeof (RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync ( int id )
             => Ok (await roleService.DeleteAsync (id));

        [HttpGet ("functionals")]
        [ProducesResponseType (typeof (List<string>), StatusCodes.Status200OK)]
        public IActionResult GetFunctionals ()
            => Ok (roleService.GetAllFunctionals ());

    }
}
