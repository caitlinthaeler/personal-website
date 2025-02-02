using data_api.Services;
using data_api.Utils;

DotEnv.Load();  // No need to pass filePath anymore

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var AllowedOrigins = new string[]
{
    "http://localhost:3000", // For local development
    "htts://localhost:3000",
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

app.UseHttpsRedirection();

// app.UseCors(AllowOriginsPolicyName);
app.UseCors("AllowSpecificOrigins");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();