
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WS_Rest1.Models;

namespace WS_Rest1.Controllers
{

    /// <summary>
    /// Controller responsável por controlar acesso e gerar JWToken
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        //classe que gera o JWT
        private readonly IJWTAuthManager jWTAuthManager;

        public SecurityController(IJWTAuthManager jWTAuthManager)
        {
            this.jWTAuthManager = jWTAuthManager;

        }

        /// <summary>
        /// Método para Autenticação...não protegido!
        /// </summary>
        /// <param name="loginDetalhes"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public AuthResponse Login(AuthRequest loginDetalhes) //or ([FromBody] AuthRequest loginDetalhes) no .NET<5.0
        {
            AuthResponse token = jWTAuthManager.Authenticate(loginDetalhes);

            if (token == null)
            {
                token = new AuthResponse();
                token.Token = Unauthorized().ToString();
            }

            return token;

            /*
             * Se retorno for IActionResult:
             *
             * if (token == null)
             *       return Unauthorized();
             *  return Ok(token);
             */
        }
    }
}
