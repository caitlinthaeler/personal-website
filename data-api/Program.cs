using data_api.Services;
using data_api.Models;

var builder = WebApplication.CreateBuilder(args);
var AllowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
var AllowOriginsPolicyName = "AllowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        AllowOriginsPolicyName, 
        policy =>
        {
            policy.WithOrigins(AllowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});
// Add services to the container.
//builder.Services.AddSingleton<WeatherService>();
//builder.Services.AddSingleton<TestService>();

var connectionString = builder.Configuration[$"MongoDB:ConnectionString"];
var databaseName = builder.Configuration["MongoDB:DatabaseName"];
var mongoConfig = builder.Configuration.GetSection("MongoDB:Collections");
var collections = mongoConfig.Get<List<Dictionary<string, string>>>();
// Register MongoDB service for each collection dynamically
foreach (var collection in collections)
{
    var collectionName = collection["collectionName"];
    var modelType = Type.GetType($"data_api.Models.{collection["modelType"]}");
    if (modelType == null)
    {
        throw new InvalidOperationException($"Model type '{modelType}' not found.");
    }
    
    builder.Services.AddSingleton(
        typeof(IDataService<>).MakeGenericType(modelType),
        sp =>
        {
            // Create an instance of MongoDBService<T> for the modelType
            var serviceType = typeof(MongoDBService<>).MakeGenericType(modelType);
            return ActivatorUtilities.CreateInstance(
                sp,
                serviceType,
                connectionString,
                databaseName,
                collectionName
            );
        } 
    );
}

builder.Services.AddSingleton<GitHubService>();

builder.Services.AddControllers();

//builder.Services.AddScoped<IDataService<Project>, MongoDBService<Project>>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(AllowOriginsPolicyName);

app.UseAuthorization();

app.MapControllers();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
