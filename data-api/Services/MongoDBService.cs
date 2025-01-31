using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace data_api.Services;
public class MongoDBService<T> : IDataService<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoDBService(string databaseName, string collectionName)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_URI");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("MONGO_URI is not configured");
        }
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllDataAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> GetDataByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task CreateDataAsync(T data)
    {
        await _collection.InsertOneAsync(data);
    }

    public async Task UpdateDataAsync(string id, T data)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        await _collection.ReplaceOneAsync(filter, data);
    }

    public async Task DeleteDataAsync(string id)
    {
        var filter =  Builders<T>.Filter.Eq("Id", id);
        await _collection.DeleteOneAsync(filter);
    }
}

