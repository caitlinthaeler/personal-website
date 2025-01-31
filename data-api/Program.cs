using data_api.Services;
using data_api.Models;
using data_api.Utils;

DotEnv.Load();  // No need to pass filePath anymore

var builder = WebApplication.CreateBuilder(args);
var AllowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
var AllowOriginsPolicyName = "AllowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        AllowOriginsPolicyName, 
        policy =>
        {
            //policy.WithOrigins(AllowedOrigins)
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
    );
});
// Add services to the container.
//builder.Services.AddSingleton<WeatherService>();
//builder.Services.AddSingleton<TestService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.MapStaticAssets();

app.UseCors(AllowOriginsPolicyName);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();