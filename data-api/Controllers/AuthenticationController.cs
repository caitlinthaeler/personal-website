using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using data_api.Models;
using data_api.Exceptions;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace data_api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtOptions _jwtOptions;
    private readonly string _username;
    private readonly string _password;
    private string _jwtToken;
    private DateTime _expiresAtUtc;
    private string _refreshToken;
    private DateTime _refreshTokenExpiresAtUtc;
    

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
            throw new LoginFailedException("invalid username");
        }
        if (request.Password != _password)
        {
            throw new LoginFailedException("invalid password");
        }

        (string jwtToken, DateTime expiresAtUtc) = GenerateJwtToken();
        _jwtToken = jwtToken;
        _expiresAtUtc = expiresAtUtc;
        _refreshToken = GenerateRefreshToken();
        _refreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7);

        
        
        WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expiresAtUtc);
        WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", _refreshToken, _refreshTokenExpiresAtUtc);
        
        return Ok(new { message = "Login successful"});
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new RefreshTokenException("Refresh token is missing");
        }

        if (_refreshTokenExpiresAtUtc < DateTime.UtcNow)
        {
            throw new RefreshTokenException("Refresh token is expired");
        }

        return Ok(new {message = "refresh successful"});
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

    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, token, new CookieOptions
        {
            HttpOnly = true,
            Expires = expiration,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
        });
    }


    
}

public class LoginRequest
{
    public string Username {get; init; }
    public string Password {get; init; }
}