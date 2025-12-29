/**
 * Serviço para controlar Autenticação
 * lufer
 **/

using AuthRest.Repositories;
using AuthRest.Entities;
using System.Security.Claims;

namespace AuthRest.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository repoInstance;
        private readonly ITokenService tokenServiceInstance;

        /// <summary>
        /// userRepository é instância do repositório em "Repositories"
        /// tokenService é instância do serviço "TokenService"
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="tokenService"></param>
        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            repoInstance = userRepository;
            tokenServiceInstance = tokenService;
        }

        /// <summary>
        /// Valida User com email e password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUserAsync(LoginRequest request)
        {
            //se email existe
            var user = await repoInstance.GetByEmailAsync(request.Email);
            if (user is null) return false;

            //analisa a sua password cifrada
            return BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        }


        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            // 1. validar parametros
            if (request == null ||
                        string.IsNullOrWhiteSpace(request.Email) ||
                        string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }

            // 2. Identificar user
            var user = await repoInstance.GetByEmailAsync(request.Email);
            if (user == null)
                return null;

            // 3. Validar password
            bool validPassword = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!validPassword)
                return null;

            // 4. Generar JWT
            return tokenServiceInstance.GenerateToken(user);
        }


        public UserDetailsResponse GetUserDetails(ClaimsPrincipal principal)
        {
            var id = principal.FindFirst("Id")?.Value;
            var email = principal.FindFirst("Email")?.Value;
            //role
            var role = principal.FindFirst(ClaimTypes.Role)?.Value;
            //polocy
            var age = principal.FindFirst("Age")?.Value;

            if (id == null || email == null || role == null || age==null)
                throw new UnauthorizedAccessException("Invalid token claims");

            return new UserDetailsResponse
            {
                Id = Guid.Parse(id),
                Email = email,
                Role = role,
                Age = int.Parse(age)
            };
        }
    }

}
