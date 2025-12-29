using AuthRest.Entities;

namespace AuthRest.Services
{
    public interface ITokenService
    {
        public AuthResponse? GenerateToken(User user);
    }
}
