using MongoDB.Driver;
using DotNetEnv;

namespace HabbitTrackerRodrigo.Services
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService()
        {
            // Estabelecer parametros de conexão com o MongoDB
            var connectionString = Environment.GetEnvironmentVariable("MongoDBConnection");
            var mongoURL = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoURL);
            _database = mongoClient.GetDatabase("UserLog"); 
        }

        public IMongoDatabase Database => _database;
    }
}
