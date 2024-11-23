using Domain;
using Domain.Nutrition;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interfaces
{
    public interface ITrainingDbContext
    {
        DbSet<ConsumedProduct> ConsumedProducts { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<FoodType> FoodTypes { get; set; }
        DbSet<Meal> Meals { get; set; }
        DbSet<Training> Trainings { get; set; }
        DbSet<MuscleGroup> MuscleGroups { get; set; }
        DbSet<ExerciseSet> ExerciseSets { get; set; }
        DbSet<Exercise> Exercises { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
