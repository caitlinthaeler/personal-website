using data_api.Services;
using data_api.Models;
using data_api.Exceptions;
using data_api.Utils;
using data_api.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Net;
using System.Security.Authentication;

DotEnv.Load();  // No need to pass filePath anymore

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var AllowedOrigins = new string[]
{
    "http://localhost:3000", // For local development
    "https://localhost:3000",
    "https://caitlinthaeler.com" // For production
};

// builder.Services.Configure<CookiePolicyOptions>(options =>
// {
//     options.Secure = CookieSecurePolicy.Always;
//     options.MinimumSameSitePolicy = SameSiteMode.None;
// });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(AllowedOrigins)
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});

builder.Services.Configure<JwtOptions>(options =>
{
    options.Secret = Environment.GetEnvironmentVariable("JWT_KEY");
    options.Issuer = Environment.GetEnvironmentVariable("JWT_ISS");
    options.Audience = Environment.GetEnvironmentVariable("JWT_AUD");
    string expTimeString = Environment.GetEnvironmentVariable("JWT_EXP_TIME_MIN");
    if (!int.TryParse(expTimeString, out int expTime))
    {
        expTime = 60;
    }
    options.ExpirationTimeInMinutes = expTime;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AuthenticationController>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISS"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUD"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["ACCESS_TOKEN"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSingleton(serviceProvider =>
{
    var clientName = "data-api";
    var token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
    var owner = config["GitHub:Owner"];
    var repo = config["GitHub:RepositoryName"];
    return new GitHubService(clientName, token, owner, repo);
});

builder.Services.AddSingleton(serviceProvider => 
{
    var databaseName = config["MongoDB:DatabaseName"];
    var connectionString = Environment.GetEnvironmentVariable("MONGO_URI");
    var gitHubService = serviceProvider.GetRequiredService<GitHubService>();
    return new MongoDBService(databaseName, connectionString);
});

builder.Services.AddSingleton<JsonFileService>();
builder.Services.AddSingleton<UpdateService>();

builder.Services.AddControllers();

//builder.Services.AddScoped<IDataService<Project>, MongoDBService<Project>>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "data_api",
        Version = "v1"
    });
});



var app = builder.Build();
await app.Services.GetRequiredService<UpdateService>().UpdateDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigins");

app.UseRouting();
app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();