/*
 * lufer
 * ISI
 * OAuth
 * REST Services
 * */
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthCore.Helpers;
using AuthCore.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthCore.Services;

public class AuthService
{
    public string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AuthSettings.PrivateKey);//class AuthSettings criada
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);    //algoritmo HMAC-SHA256 

        //Inf para criar o JWT: Subject, Expires, SigningCredentials
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(15),   //expira ao fim de 15 minutos
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    /// <summary>
    /// Define Claims (Profiles)
    /// Each role is added as a separate declaration of type ClaimTypes.Role
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    //private static List<Claim> GenerateClaims(User user)
    ////or
    ////private dynamic GenerateClaims(User user)
    //{
    //    List<Claim> claims = new List<Claim>();
    //    claims.Add(new Claim("Name", user.Name));
    //    claims.Add(new Claim("Email", user.Email));
    //    claims.Add(new Claim("Id", user.Id.ToString()));

    //    //Se houver Roles
    //    foreach (var role in user.Roles)
    //        claims.Add(new Claim(ClaimTypes.Role, role));

    //    return claims;
    //}
    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

        foreach (var role in user.Roles)
            claims.AddClaim(new Claim(ClaimTypes.Role, role));

        return claims;
    }


}