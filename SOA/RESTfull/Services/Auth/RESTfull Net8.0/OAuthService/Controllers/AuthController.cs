/*
 * Controller
 * Gere Autenticação
 * lufer
 */
  
using Microsoft.AspNetCore.Mvc;
using AuthRest.Services;
using AuthRest.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AuthRest.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authServiceInstance;

        public AuthController(IAuthService authService)
        {
            authServiceInstance = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await authServiceInstance.LoginAsync(request);

            if (response == null)
                return Unauthorized("Invalid email or password");

            return Ok(response);
        }

        [Authorize(Roles = "User")]         //Role: User
        [HttpGet("secure-user")]
        public IActionResult SecureUser()
        {
            return Ok("PERFECT!!! SECURE USER DETAILS: Only users with role User can access");
        }


        //Role=Admin or (Role=User > 18 anos)
        //[Authorize(Roles = "Admin")]        //Admin AND AdultUser
        [Authorize(Policy = "AdultUser")]   //Policy: User + Age>18
        [HttpGet("me")]
        public IActionResult GetUserDetails()
        {
            var details = authServiceInstance.GetUserDetails(User);
            return Ok(details);
        }

        //Auxiliar
        [Authorize(Roles ="Admin")]
        [HttpGet("debug")]
        public IActionResult Debug()
        {
            //return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
            return Ok(new
            {
                IsUser = User.IsInRole("User"),
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }

}
