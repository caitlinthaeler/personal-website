using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using data_api.Models;

namespace data_api.Controllers;

[ApiController]
public class MongoDatabasesController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public MongoDatabasesController(IMongoClient mongoClient, IConfiguration configuration)
    {
        var databaseName = configuration.GetValue<string>("MongoDB:DatabaseName");
        var _database = mongoClient.GetDatabase(databaseName);
    }

    [HttpGet("collections")]
    public async Task<IActionResult> ListCollections()
    {
        try
        {
            var cursor = await _database.ListCollectionNamesAsync();
            var collectionNames = await cursor.ToListAsync();
            return Ok(collectionNames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to retrieve collection names. Error: {ex.Message}");
        }
    }
}