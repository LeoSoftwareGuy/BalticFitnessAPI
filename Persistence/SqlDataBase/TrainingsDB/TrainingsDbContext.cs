﻿using Application.Data;
using Domain;
using Domain.Nutrition;
using Microsoft.EntityFrameworkCore;

namespace Persistence.SqlDataBase.TrainingsDB
{
    public class TrainingsDbContext : DbContext, ITrainingDbContext
    {
        public DbSet<ConsumedProduct> ConsumedProducts { get; set; }
        public DbSet<Product> Products { get; set; }    
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseSet> ExerciseSets { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }

        public TrainingsDbContext(DbContextOptions<TrainingsDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrainingsDbContext).Assembly);

            // Lowercase table names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());

                // Lowercase column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }
        }

    }
}
