
using AuthRest.Entities;
using System.Security.Claims;

namespace AuthRest.Services
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(LoginRequest request);

        Task<AuthResponse?> LoginAsync(LoginRequest request);

        UserDetailsResponse GetUserDetails(ClaimsPrincipal user);
    }
}
