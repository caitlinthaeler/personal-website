using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace data_api.Services
{
    public class MongoDBService<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDBService(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<T>(collectionName);
        }
    }
}
