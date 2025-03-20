using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using data_api.Models;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace data_api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly string _username;
    private readonly string _password;
    private readonly JwtOptions _jwtOptions;

    public AuthenticationController(IOptions<JwtOptions> jwtOptions, string username, string password)
    {
        _username = username;
        _password = password;
        _jwtOptions = jwtOptions.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request.Username != _username)
        {
            return Unauthorized ( new { message = "invalid username"});
        }
        if (request.Password != _password)
        {
            return Unauthorized ( new { message = "invalid password"});
        }

        (string tokenHandler, DateTime expiresAtUtc) = GenerateJwtToken();
        Response.Cookies.Append("AuthToken", tokenHandler, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
        });
        return Ok(new { message = "Login successful"});
    }

    private (string tokenHandler, DateTime expiresAtUtc) GenerateJwtToken()
    {
        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.Secret));

        var credentials = new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new[] {new Claim(JwtRegisteredClaimNames.Sub, _username)};

        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationTimeInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        
        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
        return (tokenHandler, expires);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return Ok(new { message = "Logged out" });
    }

    
}

public class LoginRequest
{
    public string Username {get; init; }
    public string Password {get; init; }
}