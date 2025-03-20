using data_api.Services;
using data_api.Models;
using data_api.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

DotEnv.Load();  // No need to pass filePath anymore

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var AllowedOrigins = new string[]
{
    "http://localhost:3000", // For local development
    "https://localhost:3000",
    "https://caitlinthaeler.com" // For production
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(AllowedOrigins)
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    AddJwtBearer(options =>
    {
        
    }).
    options.DefaultAuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

});

builder.Services.Configure<AdminDetails>(data =>
{
    data.username = Environment.GetEnvironmentVariable("ADMIN_USER");
    data.password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
});


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

app.UseAuthorization();

app.MapControllers();

app.Run();