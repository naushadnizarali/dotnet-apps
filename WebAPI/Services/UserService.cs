using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI;

public class UserService(IConfiguration configuration) : IUserService
{
    private List<JwtPayload> _users =
    [
        new() { Username = "admin", Password = "admin" }
    ];

    private readonly IConfiguration _configuration = configuration;

    public string Login(JwtPayload jwtPayload)
    {
        var loginUser = _users.SingleOrDefault(x => x.Username == jwtPayload.Username && x.Password == jwtPayload.Password);

        if (loginUser == null)
        {
            return string.Empty;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt.Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[] {
                new(ClaimTypes.Name, jwtPayload.Username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        string accessToken = tokenHandler.WriteToken(token);

        return accessToken;
    }

    public List<JwtPayload> All()
    {
        return _users;
    }
}
