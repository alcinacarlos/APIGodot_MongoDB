using MongoDB.Driver;
using System;

namespace APIGodot.Models
{
    public class MongoPlayerContext
    {
        private readonly IMongoDatabase _database;

        public MongoPlayerContext()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("ApiGodot");
        }

        public IMongoCollection<Player> Players => _database.GetCollection<Player>("Players");
    }
}