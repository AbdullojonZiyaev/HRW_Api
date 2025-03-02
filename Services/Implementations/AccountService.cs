﻿using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models.Common;
using HRM_Project.Models.DB;
using HRM_Project.Models.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRM_Project.Services.Implementations
{
    public class AccountService(
        IOptions<JwtOptions> options,
        IPasswordHasher<BaseUser> passwordHasher,
        ApplicationDbContext context,
        IMapper mapper,
        IServiceProvider serviceProvider
            ) : IAccountService
    {
        readonly JwtOptions options = options.Value;

        public JsonWebTokenDto SignIn(SignInDto signInObj)
        {
            var user = context.Users
                .Where(x => !x.IsDeleted && x.Username == signInObj.Username)
                .Select(u => mapper.Map<BaseUser>(u))
                .SingleOrDefault();
            if (user is null || !BCrypt.Net.BCrypt.Verify(signInObj.Password, user.Password))
                throw new ToException(ToErrors.INVALID_CREDENTIALS);

            var jwt = generateJWT(user);

            var refreshTokenObj = context.RefreshTokens.SingleOrDefault(x => x.Username == user.Username);
            if (refreshTokenObj is not null)
                refreshTokenObj.Refresh(jwt.RefreshToken, options.ExpiryRefreshTokenDays);
            else
                context.Add(new RefreshToken(user.Username, jwt.RefreshToken, options.ExpiryRefreshTokenDays));
            
            context.SaveChanges();
            return jwt;
        }
        public JsonWebTokenDto RefreshAccessToken(string refreshToken)
        {
            var refreshTokenObj = context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);
            if (refreshTokenObj is null)
                throw new ToException(ToErrors.REFRESH_TOKEN_NOT_FOUND);
            if(refreshTokenObj.Expires  < DateTime.Now)
            {
                context.Remove(refreshTokenObj);
                context.SaveChanges();
                throw new ToException(ToErrors.EXPIRED_REFRESH_TOKEN);
            }

            var user = context.Users
                .Where(u => !u.IsDeleted && u.Username == refreshTokenObj.Username)
                .Select(u => mapper.Map<BaseUser>(u))
                .SingleOrDefault();

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            var jwt = generateJWT(user);

            refreshTokenObj.Refresh(jwt.RefreshToken, options.ExpiryRefreshTokenDays);
            context.SaveChanges();
            return jwt;
        }
        public void RevokeRefreshToken(string username)
        {
            var refreshTokenObj = context.RefreshTokens.SingleOrDefault(x => x.Username == username);
            if (refreshTokenObj is not null)
            {
                context.Remove(refreshTokenObj);
                context.SaveChanges();
            }
        }
        public void SignOut()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var tokenManager = scope.ServiceProvider.GetService<ITokenManager>();
                var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
                var username = httpContextAccessor.HttpContext.User.Identity.Name;
                RevokeRefreshToken(username);
                tokenManager.DeactivateCurrentAsync().Wait();
            }
        }

        JsonWebTokenDto generateJWT(BaseUser user)
        {
            var expires = DateTime.UtcNow.AddMinutes(options.ExpiryMinutes);
            var exp = (long)new TimeSpan(expires.Ticks - new DateTime(1970, 1, 1).Ticks).TotalSeconds;
                
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("unique_name", user.Username),
            };
            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                claims: userClaims,
                expires: expires,
                signingCredentials: credentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
            return new JsonWebTokenDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expires = exp
            };
        }
        public string Username()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

                if (httpContextAccessor == null || httpContextAccessor.HttpContext == null)
                {
                    throw new ToException(ToErrors.USER_NOT_FOUND);
                }

                var user = httpContextAccessor.HttpContext.User;
                if (user?.Identity?.IsAuthenticated == false)
                {
                    throw new ToException(ToErrors.USER_NOT_FOUND);
                }

                var username = user?.Identity?.Name;

                if (string.IsNullOrEmpty(username))
                {
                    throw new ToException(ToErrors.USER_NOT_FOUND);
                }

                return username;
            }
        }

    }
}
