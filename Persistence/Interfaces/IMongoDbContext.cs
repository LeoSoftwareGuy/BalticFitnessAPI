using Domain;
using Domain.Nutrition;
using MongoDB.Driver;

namespace Persistence.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<MuscleGroup> MuscleGroups { get; }
        IMongoCollection<Exercise> Exercises { get; }
        IMongoCollection<Training> Trainings { get; }
        IMongoCollection<FoodType> FoodTypes { get; }
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Meal> Meals { get; }
        IMongoClient MongoClient { get; }
    }
}
