using Domain;
using Domain.Nutrition;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Persistence.MongoDatabase
{
    /// <summary>
    /// This Database configuration is responsible for every entity of the application BUT NOT for the user and token entities.
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient mongoClient, IConfiguration configuration)
        {
            var mongoConfig = configuration.GetSection("Mongo");
            _database = mongoClient.GetDatabase(mongoConfig.GetSection("DatabaseName").Value);
            MongoClient = mongoClient;
        }

        public IMongoCollection<MuscleGroup> MuscleGroups => _database.GetCollection<MuscleGroup>("muscles");
        public IMongoCollection<Exercise> Exercises => _database.GetCollection<Exercise>("exercises");
        public IMongoCollection<Training> Trainings => _database.GetCollection<Training>("trainings");
        public IMongoCollection<FoodType> FoodTypes => _database.GetCollection<FoodType>("foodTypes");
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
        public IMongoCollection<Meal> Meals => _database.GetCollection<Meal>("meals");
  
        public IMongoClient MongoClient { get; }
    }
}
