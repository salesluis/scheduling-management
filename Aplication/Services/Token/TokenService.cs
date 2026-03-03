using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Aplication.Services.Token;

public class TokenService
{
    public string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.KEY);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires =  DateTime.UtcNow.AddHours(5),
            SigningCredentials =  new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature),
            
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}