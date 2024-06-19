using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;

namespace HRM_Project.Services
{
    public interface IAccountService
    {
        JsonWebTokenDto SignIn ( SignInDto signInObj );
        JsonWebTokenDto RefreshAccessToken ( string refreshToken );
        void RevokeRefreshToken ( string username );
        void SignOut ();
        string Username ();
    }
}
