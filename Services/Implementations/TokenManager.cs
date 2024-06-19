using HRM_Project.Models.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace HRM_Project.Services.Implementations
{
    public class TokenManager(
            IDistributedCache cache,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtOptions> jwtOptions
            ) : ITokenManager
    {
        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await cache.GetStringAsync(GetKey(token)) == null;

        public async Task DeactivateAsync(string token)
            => await cache.SetStringAsync(GetKey(token),
                " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(jwtOptions.Value.ExpiryMinutes)
                });

        string GetCurrentAsync()
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];
            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        string GetKey(string token)
            => $"tokens:{token}:deactivated";
    }
}
