using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace data_api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly string _adminUsername;
    private readonly string _adminPassword;

    public AuthenticationController(string adminUsername, string adminPassword)
    {
        _adminUsername = adminUsername;
        _adminPassword = adminPassword;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request.Username != _adminUsername)
        {
            return Unauthorized ( new { message = "invalid username"});
        }
        if (request.Password != _adminPassword)
        {
            return Unauthorized ( new { message = "invalid password"});
        }

        string token = GenerateJwtToken(request.Username);
        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
        });
        return Ok(new { message = "Login successful"});
    }

    private string GenerateJwtToken(string username)
    {
        byte[] key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"));

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username)}),
            Issuer = Environment.GetEnvironmentVariable("JWT_ISS"),
            Audience = Environment.GetEnvironmentVariable("JWT_AUD"),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new { message = "Logged out" });
    }

    
}

public class LoginRequest
{
    public string Username {get; init; }
    public string Password {get; init; }
}