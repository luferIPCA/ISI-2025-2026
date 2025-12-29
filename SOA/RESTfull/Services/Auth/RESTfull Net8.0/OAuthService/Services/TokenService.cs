using Microsoft.IdentityModel.Tokens;
using AuthRest.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthRest.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configInstance;

        public TokenService(IConfiguration conf)
        {
            configInstance = conf;
        }

        public AuthResponse? GenerateToken(User user)
        {
            //Armazenar Claims: Credenciais, Roles, Policies
            List<Claim> claims = new List<Claim>();

            //criar credenciais
            claims.Add(new Claim("Email", user.Email));
            claims.Add(new Claim("Id", user.Id.ToString()));
            
            //add roles
            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            //Se user tem vários Roles
            //foreach (var role in user.Roles)
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            
            //add policy age
            claims.Add(new Claim("Age", user.Age.ToString()));
            // (Optional, mas recomendado)
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


            //3º Criar chave de segurança
            //SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]);
            //or
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configInstance["Jwt:Key"]!)); //"!" em "config["Jwt:Key"]!" remove o warming!!!
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Definir duração do token..está no json externo... (nos tokens usar DateTime.UtcNow, em vez de DateTime.Now)
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(configInstance["Jwt:ExpireMinutes"]!)
            );

            //4º criar token
            var tokenData = new JwtSecurityToken(
                issuer: configInstance["Jwt:Issuer"],
                audience: configInstance["Jwt:Audience"],
                signingCredentials: creds,
                claims: claims,
                expires: expires);

            //5º esrever o token
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            //6º devolver a resposta
            return new AuthResponse(user, token, expires);
        }
    }

}
