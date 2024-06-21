using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM_Project.Controllers
{
    [Route ("api/account")]
    [ApiController]
    //[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController(IAccountService accountService, IDemoService demoService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost ("sign-in")]
        [ProducesResponseType (typeof (JsonWebTokenDto), StatusCodes.Status200OK)]
        public IActionResult SignIn ( [FromBody] SignInDto request )
           => Ok (accountService.SignIn (request));

        [AllowAnonymous]
        [HttpPost ("token/{refreshToken}/refresh")]
        [ProducesResponseType (typeof (JsonWebTokenDto), StatusCodes.Status200OK)]
        public IActionResult RefreshAccessToken ( string refreshToken )
            => Ok (accountService.RefreshAccessToken (refreshToken));

        [HttpPost ("sign-out")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public new void SignOut ()
            => accountService.SignOut ();
        [AllowAnonymous]
        [HttpPost ("demo")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public async Task CreateDemoAsync ()
            => await demoService.CreateDemoAsync ();
    }
}
