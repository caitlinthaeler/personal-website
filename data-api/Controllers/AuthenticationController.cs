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
    //private Dictionary<string, string> _trackedCookies = new();
    private string _jwtToken;
    private DateTime _expiresAtUtc;
    private string _refreshToken;
    private DateTime _refreshTokenExpiresAtUtc;
    

    public AuthenticationController(IHttpContextAccessor httpContextAccessor, IOptions<JwtOptions> jwtOptions)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _username = Environment.GetEnvironmentVariable("ADMIN_USERNAME");;
        _password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");;
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

        //WriteAuthTokenAsHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expiresAtUtc);
        //WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", _refreshToken, _refreshTokenExpiresAtUtc);
        //var token = _trackedCookies["ACCESS_TOKEN"];
        //Console.WriteLine("checking token: "+token);
        // Console.WriteLine("login successful");
        // Console.WriteLine("number of cookies before login successful: "+_trackedCookies.Count);
        // return Ok(new { message = "Login successful"});
        return Ok(new {token = jwtToken});
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

    [HttpGet("validate")]
    public IActionResult ValidateToken()
    {
        string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //Console.WriteLine("number of cookies when validating: "+_trackedCookies.Count);
        //string token = _trackedCookies["ACCESS_TOKEN"];
        //Request.Cookies["ACCESS_TOKEN"];
        if (string.IsNullOrEmpty(token))
        {
            throw new TokenInvalidException("No token found");
        }

        bool isValid = ValidateToken(token);

        if (!isValid)
        {
            //DeleteAuthCookies();
            throw new TokenInvalidException("Token is invalid");
        }
        return Ok(new { message = "Token is valid"});
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        //DeleteAuthCookies();
        return Ok(new { message = "Logged out" });
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

    

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            byte[] key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // public void DeleteAuthCookies()
    // {
    //     Console.WriteLine("deleting cookies");
    //     var response =  _httpContextAccessor.HttpContext?.Response;
    //     if (response != null)
    //     {
    //         Console.WriteLine("found some cookies i think");
    //         foreach (KeyValuePair<string, string> cookie in _trackedCookies)
    //         {
    //             Console.WriteLine(cookie.Value);
    //             response.Cookies.Delete(cookie.Value);
    //         }
    //         _trackedCookies.Clear();
    //     }
    // }

    public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration)
    {
        // _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, token, new CookieOptions
        // {
        //     HttpOnly = true,
        //     Expires = expiration,
        //     IsEssential = true,
        //     Secure = false,
        //     SameSite = SameSiteMode.None,
        // });
        // Console.WriteLine($"Setting cookie: {cookieName} = {token}");

        var response =  _httpContextAccessor.HttpContext?.Response;
        if (response != null)
        {
            Console.WriteLine("http response successful, adding cookie...");
            response.Cookies.Append(cookieName, token, new CookieOptions
            {
                HttpOnly = true,
                Expires = expiration,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            });
            //_trackedCookies[cookieName] = token;
            //Console.WriteLine("number of cookies: "+_trackedCookies.Count);
            Console.WriteLine($"Setting cookie: {cookieName} = {token}");
        }
        
        
    }
}

public class LoginRequest
{
    public string Username {get; init; }
    public string Password {get; init; }
}