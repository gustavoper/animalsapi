using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnimalsApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace AnimalsApi.Services
{
    public static class TokenService
    {
        public static string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            //var key = Encoding.ASCII.GetBytes(Settings.JwtSecretKey);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.username.ToString()),
                    //new Claim("teste","novainfoaqui")
                    //new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Settings.JwtSecretKey)
                    ), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var jwtToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(jwtToken);
        }
    }
}