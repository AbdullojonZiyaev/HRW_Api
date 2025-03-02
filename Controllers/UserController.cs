﻿using HRM_Project.DTOs.Params;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
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
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType (typeof (PagedList<User, UserViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync ( [FromQuery] NameAndPagedParam param )
           => Ok (await userService.SearchAsync (param));

        [HttpPost ("add")]
        [ProducesResponseType (typeof (UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync ( [FromBody] UserCreateDto create )
            {
            try
            {
                Console.WriteLine("AddAsync called with data: ");
                return Ok(await userService.AddAsync(create));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AddAsync: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut ("edit")]
        [ProducesResponseType (typeof (UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync ( [FromBody] UserUpdateDto update )
            => Ok (await userService.UpdateAsync (update));

        [HttpDelete ("{id}")]
        [ProducesResponseType (typeof (UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync ( int id )
             => Ok (await userService.DeleteAsync (id));

        [HttpGet ("info")]
        [ProducesResponseType (typeof (UserInfoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfoAsync ()
             => Ok (await userService.GetUserInfoAsync ());

        [HttpPut ("change-password")]
        [ProducesResponseType (typeof (UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePasswordAsync ( [FromBody] ChangePasswordDto change )
            => Ok (await userService.ChangePasswordAsync (change));

        [HttpGet("current")]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentAsync()
    => Ok(await userService.GetCurrentAsync());

        [HttpGet("by-username/{username}")]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUsernameAsync(string username)
        => Ok(await userService.GetByUsernameAsync(username));

    }
}
