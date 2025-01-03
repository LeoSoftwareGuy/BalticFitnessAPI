﻿using Application.Data;
using AutoMapper;
using Domain;

namespace Application.System.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly ITrainingDbContext _dbContext;

        public SampleDataSeeder(ITrainingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            var exercises = new Exercise[]
            {
                 new Exercise
                 {
                   Id = 1,
                   MuscleGroupId = 1,
                   Name = "Bench Press",
                   ImageUrl = "img/chest/BenchPress.jpg"
                 },
                 new Exercise
                 {
                    Id = 2,
                    MuscleGroupId = 1,
                    Name = "Cable Cross Over",
                    ImageUrl = "img/chest/CableCrossOver.jpg"
                 },
                 new Exercise
                 {
                    Id = 3,
                    MuscleGroupId = 1,
                    Name = "Single Handed Bench Press",
                    ImageUrl = "img/chest/BenchPressOneHand.jpg"
                 },
                 new Exercise
                 {
                    Id = 4,
                    MuscleGroupId = 1,
                    Name = "Hammer Flyies",
                    ImageUrl = "img/chest/ChestHammerFlyes.jpg"
                 },
                 new Exercise
                 {
                    Id = 5,
                    MuscleGroupId = 1,
                    Name = "Chest Pull Over",
                    ImageUrl = "img/chest/ChestPullOver.jpg"
                 },
                 new Exercise
                 {
                    Id = 6,
                    MuscleGroupId = 1,
                    Name = "Dips",
                    ImageUrl = "img/chest/Dips.jpg"
                 },
                 new Exercise
                 {
                     Id = 7,
                     MuscleGroupId = 1,
                     Name="Flat Barbell Press",
                     ImageUrl = "img/chest/FlatBarbellPress.jpg"
                 },
                 new Exercise
                 {
                    Id = 8,
                    MuscleGroupId = 1,
                    Name = "Incline Bench Press",
                    ImageUrl = "img/chest/InclineBench.jpg"
                 },
                new Exercise
                {
                    Id = 9,
                    MuscleGroupId = 1,
                    Name = "Incline Dumbbell Press",
                    ImageUrl = "img/chest/InclineDumbelPress.jpg"
                },
                new Exercise
                {
                    Id = 10,
                    MuscleGroupId = 1,
                    Name = "Incline Flyies",
                    ImageUrl = "img/chest/InclineFly.jpg"
                },
                new Exercise
                {
                    Id = 11,
                    MuscleGroupId = 1,
                    Name = "Cable Flyies Bottom - Top",
                    ImageUrl = "img/chest/LowCableFly.jpg"
                },
                new Exercise
                {
                    Id = 12,
                    MuscleGroupId = 2,
                    Name="T-Bar Rows",
                    ImageUrl = "img/back/TBarRows.jpg"
                },
                new Exercise
                {
                    Id = 13,
                    MuscleGroupId = 2,
                    Name="Back Pull Over",
                    ImageUrl = "img/back/BackPullOver.jpg"
                },
                new Exercise
                {
                    Id = 14,
                    MuscleGroupId = 2,
                    Name="Barbell Shrugs",
                    ImageUrl = "img/back/BarbellShrugs.jpg"
                },
                new Exercise
                {
                    Id = 15,
                    MuscleGroupId = 2,
                    Name="Horizontal Cable Rows",
                    ImageUrl = "img/back/HorizontalCableRow.jpg"
                },
                new Exercise
                {
                    Id = 16,
                    MuscleGroupId = 2,
                    Name="Hyper Extensions",
                    ImageUrl = "img/back/HyperExtensions.jpg"
                },
                new Exercise
                {
                    Id = 17,
                    MuscleGroupId = 2,
                    Name="Incline Bench Row",
                    ImageUrl = "img/back/InclineBenchRow.jpg"
                },
                new Exercise
                {
                    Id = 18,
                    MuscleGroupId = 2,
                    Name="Lats Pull Down",
                    ImageUrl = "img/back/LatPullDown.jpg"
                },
                new Exercise
                {
                    Id = 19,
                    MuscleGroupId = 2,
                    Name="Single Handed Lats Pull Down",
                    ImageUrl = "img/back/LatPullDownSingleHand.jpg"
                },
                new Exercise
                {
                    Id = 20,
                    MuscleGroupId = 2,
                    Name="Seated Row Machine",
                    ImageUrl = "img/back/SeatedRowMachine.jpg"
                },
                new Exercise
                {
                    Id = 21,
                    MuscleGroupId = 2,
                    Name="Single Arm Rows",
                    ImageUrl = "img/back/SingleArmRow.jpg"
                },
                new Exercise
                {
                    Id = 22,
                    MuscleGroupId = 3,
                    Name="Lunges With Barbells",
                    ImageUrl = "img/legs/BarbelLunges.jpg"
                },
                new Exercise
                {
                    Id = 23,
                    MuscleGroupId = 3,
                    Name="Deadlifts",
                    ImageUrl = "img/legs/Deadlift.jpg"
                },
                new Exercise
                {
                    Id = 24,
                    MuscleGroupId = 3,
                    Name="Deadlifts With Dumbbells",
                    ImageUrl = "img/legs/DumbellDeadlift.jpg"
                },
                new Exercise
                {
                    Id = 25,
                    MuscleGroupId = 3,
                    Name="Squats with Dumbbell",
                    ImageUrl = "img/legs/DumbleSqauts.jpg"
                },
                new Exercise
                {
                    Id = 26,
                    MuscleGroupId = 3,
                    Name="Goblet Squats",
                    ImageUrl = "img/legs/GobletSquats.jpg"
                },
                new Exercise
                {
                    Id = 27,
                    MuscleGroupId = 3,
                    Name="Hack Squats",
                    ImageUrl = "img/legs/HackSquat.jpg"
                },
                new Exercise
                {
                    Id = 28,
                    MuscleGroupId = 3,
                    Name="Hammstring Extensions",
                    ImageUrl = "img/legs/HammstringsExtrension.jpg"
                },
                new Exercise
                {
                    Id = 29,
                    MuscleGroupId = 3,
                    Name="Inner-Outer Thigh Machine",
                    ImageUrl = "img/legs/InnerOuterThighMachine.jpg"
                },
                new Exercise
                {
                    Id = 30,
                    MuscleGroupId = 3,
                    Name="Legs Extensions",
                    ImageUrl = "img/legs/LegExtension.jpg"
                },
                new Exercise
                {
                    Id = 31,
                    MuscleGroupId = 3,
                    Name="Legs Press",
                    ImageUrl = "img/legs/LegPress.jpg"
                },
                new Exercise
                {
                    Id = 32,
                    MuscleGroupId = 3,
                    Name="Lunges With Barbell",
                    ImageUrl = "img/legs/LungesWithBarbell.jpg"
                },
                new Exercise
                {
                    Id = 33,
                    MuscleGroupId = 3,
                    Name="Single Leg Dumbbell Split Squat",
                    ImageUrl = "img/legs/SingleLegDumbelSplitSquat.jpg"
                },
                new Exercise
                {
                    Id = 34,
                    MuscleGroupId = 3,
                    Name="Classic Squats",
                    ImageUrl = "img/legs/Squats.jpg"
                },
                 new Exercise
                {
                    Id = 35,
                    MuscleGroupId = 4,
                    Name="Arnold Press",
                    ImageUrl = "img/delts/ArnoldPress.jpg"
                },
                new Exercise
                {
                    Id = 36,
                    MuscleGroupId = 4,
                    Name="Front Delts Raises",
                    ImageUrl = "img/delts/FrontDeltRaises.jpg"
                },
                new Exercise
                {
                    Id = 37,
                    MuscleGroupId = 4,
                    Name="Rear Delts Raises",
                    ImageUrl = "img/delts/RearDeltsRaises.jpg"
                },
                new Exercise
                {
                    Id = 38,
                    MuscleGroupId = 4,
                    Name="Barbell Smith Press",
                    ImageUrl = "img/delts/ShouldBarbelSmithPress.jpg"
                },
                new Exercise
                {
                    Id = 39,
                    MuscleGroupId = 4,
                    Name="Dumbbell Press",
                    ImageUrl = "img/delts/ShouldDumbellPress.jpg"
                },
                new Exercise
                {
                    Id = 40,
                    MuscleGroupId = 4,
                    Name="Delts Hammer Press",
                    ImageUrl = "img/delts/ShoulderHammerPress.jpg"
                },
                new Exercise
                {
                    Id = 41,
                    MuscleGroupId = 4,
                    Name="Standing Lateral Raises",
                    ImageUrl = "img/delts/StandingLateralRaises.jpg"
                },

                new Exercise
                {
                    Id = 42,
                    MuscleGroupId = 5,
                    Name="Hammer for Biceps",
                    ImageUrl = "img/biceps/BicepsHammer.jpg"
                },
                new Exercise
                {
                    Id = 43,
                    MuscleGroupId = 5,
                    Name="Pull-ups for Biceps",
                    ImageUrl = "img/biceps/BicepsPullUps.jpg"
                },
                new Exercise
                {
                    Id = 44,
                    MuscleGroupId = 5,
                    Name="Scott Puls for Biceps",
                    ImageUrl = "img/biceps/BicepsScotPull.jpg"
                },
                new Exercise
                {
                    Id = 45,
                    MuscleGroupId = 5,
                    Name="Isolated Seated Biceps Curls",
                    ImageUrl = "img/biceps/IsolatedSeatedBicepsCurls.jpg"
                },
                new Exercise
                {
                    Id = 46,
                    MuscleGroupId = 5,
                    Name="Seated Biceps Dumbbell Curls",
                    ImageUrl = "img/biceps/SeatedBicepsDumbellsCurls.jpg"
                },
                new Exercise
                {
                    Id = 47,
                    MuscleGroupId = 5,
                    Name="Standing Biceps Bench Curls",
                    ImageUrl = "img/biceps/StandingBicepsBenchCurls.jpg"
                },
                new Exercise
                {
                    Id = 48,
                    MuscleGroupId = 5,
                    Name="Standing Biceps Cable Curls",
                    ImageUrl = "img/biceps/StandingBicepsCableCurls.jpg"
                },
                new Exercise
                {
                    Id = 49,
                    MuscleGroupId = 5,
                    Name="Standing Biceps Dumbbell Curls",
                    ImageUrl = "img/biceps/StandingBicepsDumbellCurls.jpg"
                },

                new Exercise
                {
                    Id = 50,
                    MuscleGroupId = 6,
                    Name = "Dips",
                    ImageUrl = "img/triceps/Dips.jpg"
                },
                new Exercise
                {
                    Id = 51,
                    MuscleGroupId = 6,
                    Name = "Lying Triceps Dumbbell Extensions",
                    ImageUrl = "img/triceps/LyingTricepsDumbellExtensions.jpg"
                },
                new Exercise
                {
                    Id = 52,
                    MuscleGroupId = 6,
                    Name = "Lying Triceps French Press",
                    ImageUrl = "img/triceps/LyingTricepsFrenchPress.jpg"
                },
                new Exercise
                {
                    Id = 53,
                    MuscleGroupId = 6,
                    Name = "Standing Overhead One Arm Cable Triceps Extension",
                    ImageUrl = "img/triceps/StandingOverheadOneArmCableTricepsExtension.jpg"
                },
                new Exercise
                {
                    Id = 54,
                    MuscleGroupId = 6,
                    Name = "Standing Triceps Overhead Cable Extension",
                    ImageUrl = "img/triceps/StandingTricepsOverheadCableExtension.jpg"
                },
               new Exercise
                {
                    Id = 55,
                    MuscleGroupId = 7,
                    Name = "Calf Raises",
                    ImageUrl = "img/calves/CalfRaises.jpg"
                },
                new Exercise
                {
                    Id = 56,
                    MuscleGroupId = 7,
                    Name = "Calf Smith Raises",
                    ImageUrl = "img/calves/CalfSmithRaises.jpg"
                },
                new Exercise
                {
                    Id = 57,
                    MuscleGroupId = 8,
                    Name="Abs Harley",
                    ImageUrl = "img/abs/AbsHarley.jpg"
                },
                new Exercise
                {
                    Id = 58,
                    MuscleGroupId = 8,
                    Name="Crunches",
                    ImageUrl = "img/abs/Crunches.jpg"
                },
                new Exercise
                {
                    Id = 59,
                    MuscleGroupId = 8,
                    Name="Lower Abs Leg Raises",
                    ImageUrl = "img/abs/LowerAbsLieRaises.jpg"
                },

            };
            var muscleGroups = new MuscleGroup[]
            {
                new MuscleGroup()
                {
                    Id = 1,
                    ImageUrl = "img/chest/Chest.jpg",
                    Name = "Chest",
                    Type = "Upper"
                },
                new MuscleGroup()
                {
                    Id = 2,
                    ImageUrl = "img/back/Back.jpg",
                    Name = "Back",
                    Type = "Upper"
                },
                new MuscleGroup()
                {
                    Id = 3,
                    ImageUrl = "img/legs/Legs.jpg",
                    Name = "Legs",
                    Type = "Lower"
                },
                new MuscleGroup()
                {
                    Id = 4,
                    ImageUrl = "img/delts/Delts.jpg",
                    Name = "Shoulders",
                    Type = "Upper"
                },
                new MuscleGroup()
                {
                    Id = 5,
                    ImageUrl = "img/biceps/Biceps.jpg",
                    Name="Biceps",
                    Type = "Upper"
                },
                new MuscleGroup()
                {
                    Id = 6,
                    ImageUrl = "img/triceps/Triceps.jpg",
                    Name = "Triceps",
                    Type = "Upper"
                },
                new MuscleGroup()
                {
                    Id = 7,
                    ImageUrl = "img/calves/Calves.jpg",
                    Name="Calves",
                    Type = "Lower"
                },
                new MuscleGroup()
                {
                    Id = 8,
                    ImageUrl = "img/abs/Abs.jpg",
                    Name="Abs",
                    Type = "Cardio"
                }
             };
            //var foodTypes = new FoodType[]
            //{
            //    new FoodType()
            //    {
            //        Id = 1,
            //        ImageUrl = "img/dairy/Dairy.jpg",
            //        Name = "Dairy",
            //        Products = new List<Product>
            //        {
            //            new Product()
            //    {
            //        Id = Guid.NewGuid(),
            //        Title = "BlueCheese",
            //        ImageUrl = "img/dairy/BlueCheese.jpg",
            //        CaloriesPer100 = 353,
            //        CarbsPer100 = 2.3f,
            //        ProteinPer100 = 21.4f,
            //        FatsPer100 = 28.7f,
            //    },
            //    new Product()
            //    {
            //              Id = Guid.NewGuid(),
            //        Title = "Chedder",
            //        ImageUrl = "img/dairy/Chedder.jpg",
            //        CaloriesPer100 = 403,
            //        CarbsPer100 = 1.2f,
            //        ProteinPer100 = 24.9f,
            //        FatsPer100 = 33.14f,
            //    },
            //    new Product()
            //    {
            //              Id = Guid.NewGuid(),
            //        Title = "CottageCheese",
            //        ImageUrl = "img/dairy/CottageCheese.png",
            //        CaloriesPer100 = 103,
            //        CarbsPer100 = 2.68f,
            //        ProteinPer100 = 12.49f,
            //        FatsPer100 = 4.51f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "CremeCheese",
            //        ImageUrl = "img/dairy/CreemeCheese.jpg",
            //        CaloriesPer100 = 349,
            //        CarbsPer100 = 2.66f,
            //        ProteinPer100 = 7.55f,
            //        FatsPer100 = 34.87f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Hallumi",
            //        ImageUrl = "img/dairy/Hallumi.jpg",
            //        CaloriesPer100 = 316,
            //        CarbsPer100 = 1.6f,
            //        ProteinPer100 = 20.8f,
            //        FatsPer100 = 24.7f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Milk",
            //        ImageUrl = "img/dairy/Milk.jpg",
            //        CaloriesPer100 = 55,
            //        CarbsPer100 = 4.8f,
            //        ProteinPer100 = 3.2f,
            //        FatsPer100 = 2.5f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Mozarella",
            //        ImageUrl = "img/dairy/Mozarella.jpg",
            //        CaloriesPer100 = 302,
            //        CarbsPer100 = 3.83f,
            //        ProteinPer100 = 26,
            //        FatsPer100 = 20,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Parmesan",
            //        ImageUrl = "img/dairy/Parmesan.jpg",
            //        CaloriesPer100 = 401,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 35.2f,
            //        FatsPer100 = 29.4f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "SweetCurd",
            //        ImageUrl = "img/dairy/SweetCurd.jpg",
            //        CaloriesPer100 = 298,
            //        CarbsPer100 = 22,
            //        ProteinPer100 = 14,
            //        FatsPer100 = 19,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Yogurt",
            //        ImageUrl = "img/dairy/Yogurt.jpg",
            //        CaloriesPer100 = 78,
            //        CarbsPer100 = 7,
            //        ProteinPer100 = 6,
            //        FatsPer100 = 3,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "MilletPorridge",
            //        ImageUrl = "img/dairy/MilletPoridge.jpg",
            //        CaloriesPer100 = 136,
            //        CarbsPer100 = 28.5f,
            //        ProteinPer100 = 3.1f,
            //        FatsPer100 = 1.0f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "OatMeal",
            //        ImageUrl = "img/dairy/OatMeal.jpg",
            //        CaloriesPer100 = 166,
            //        CarbsPer100 = 28,
            //        ProteinPer100 = 5.9f,
            //        FatsPer100 = 3.6f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "RicePorridge",
            //        ImageUrl = "img/dairy/RicePoridge.jpg",
            //        CaloriesPer100 = 113,
            //        CarbsPer100 = 15.6f,
            //        ProteinPer100 = 2.6f,
            //        FatsPer100 = 4.4f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Semolina",
            //        ImageUrl = "img/dairy/Semolina.jpg",
            //        CaloriesPer100 = 98,
            //        CarbsPer100 = 15.3f,
            //        ProteinPer100 = 3,
            //        FatsPer100 = 3.2f,
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 2,
            //        ImageUrl = "img/eggs/BoiledEggs.jpg",
            //        Name = "Eggs",
            //        Products = new List<Product>
            //        {
            //             new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "BoiledEggs",
            //        ImageUrl = "img/eggs/BoiledEggs.jpg",
            //        CaloriesPer100 = 154,
            //        CarbsPer100 = 1,
            //        ProteinPer100 = 12.5f,
            //        FatsPer100 = 10.6f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "FriedEggs",
            //        ImageUrl = "img/eggs/FriedEggs.jpg",
            //        CaloriesPer100 = 201,
            //        CarbsPer100 = 1,
            //        ProteinPer100 = 13.6f,
            //        FatsPer100 = 15.3f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Omlet",
            //        ImageUrl = "img/eggs/Omlet.jpg",
            //        CaloriesPer100 = 153,
            //        CarbsPer100 = 1,
            //        ProteinPer100 = 14,
            //        FatsPer100 = 12,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "ScrambledEggs",
            //        ImageUrl = "img/eggs/ScrambledEggs.jpg",
            //        CaloriesPer100 = 166,
            //        CarbsPer100 = 3,
            //        ProteinPer100 = 16,
            //        FatsPer100 = 12,
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 3,
            //        ImageUrl = "img/fish/Fish.jpg",
            //        Name = "Fish",
            //        Products = new List<Product>
            //        {
            //            new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Cod",
            //        ImageUrl = "img/fish/Cod.jpg",
            //        CaloriesPer100 = 105,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 23,
            //        FatsPer100 = 1,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "FriedFish",
            //        ImageUrl = "img/fish/FriedFish.jpg",
            //        CaloriesPer100 = 220,
            //        CarbsPer100 = 10,
            //        ProteinPer100 = 21,
            //        FatsPer100 = 10,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Prawn",
            //        ImageUrl = "img/fish/Prawn.jpg",
            //        CaloriesPer100 = 105,
            //        CarbsPer100 = 1,
            //        ProteinPer100 = 20,
            //        FatsPer100 = 1.7f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Salmon",
            //        ImageUrl = "img/fish/Salmon.jpg",
            //        CaloriesPer100 = 211,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 34,
            //        FatsPer100 = 7.3f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Tiger_Prawn",
            //        ImageUrl = "img/fish/Tiger_Prawn.jpg",
            //        CaloriesPer100 = 105,
            //        CarbsPer100 = 1,
            //        ProteinPer100 = 20,
            //        FatsPer100 = 1.7f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Tuna",
            //        ImageUrl = "img/fish/Tuna.jpg",
            //        CaloriesPer100 = 130,
            //        CarbsPer100 = 7,
            //        ProteinPer100 = 10.4f,
            //        FatsPer100 = 6.7f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "BreadedFish",
            //        ImageUrl = "img/fish/fishAndChips.jpg",
            //        CaloriesPer100 = 199,
            //        CarbsPer100 = 7,
            //        ProteinPer100 = 16.72f,
            //        FatsPer100 = 11.4f,
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 4,
            //        ImageUrl = "img/fruits/Fruits.jpg",
            //        Name = "Fruits",
            //        Products = new List<Product>
            //        {
            //             new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Apple",
            //        ImageUrl = "img/fruits/Apple.jpg",
            //        CaloriesPer100 = 52,
            //        CarbsPer100 = 14,
            //        ProteinPer100 = 0.3f,
            //        FatsPer100 = 0.2f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Avocado",
            //        ImageUrl = "img/fruits/Avocado.jpg",
            //        CaloriesPer100 = 160,
            //        CarbsPer100 = 8.5f,
            //        ProteinPer100 = 2,
            //        FatsPer100 = 15,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Bananna",
            //        ImageUrl = "img/fruits/Banana.jpg",
            //        CaloriesPer100 = 89,
            //        CarbsPer100 = 23,
            //        ProteinPer100 = 1.1f,
            //        FatsPer100 = 0.3f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Coconut",
            //        ImageUrl = "img/fruits/Coconut.jpg",
            //        CaloriesPer100 = 148,
            //        CarbsPer100 = 15.23f,
            //        ProteinPer100 = 3.3f,
            //        FatsPer100 = 33.5f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Mango",
            //        ImageUrl = "img/fruits/Mango.jpg",
            //        CaloriesPer100 = 60,
            //        CarbsPer100 = 15,
            //        ProteinPer100 = 1,
            //        FatsPer100 = 0.4f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Orange",
            //        ImageUrl = "img/fruits/Orange.jpg",
            //        CaloriesPer100 = 43,
            //        CarbsPer100 = 13,
            //        ProteinPer100 = 1,
            //        FatsPer100 = 0,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Pear",
            //        ImageUrl = "img/fruits/Pear.jpg",
            //        CaloriesPer100 = 57,
            //        CarbsPer100 = 15,
            //        ProteinPer100 = 0,
            //        FatsPer100 = 0,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Plums",
            //        ImageUrl = "img/fruits/Plums.jpg",
            //        CaloriesPer100 = 46,
            //        CarbsPer100 = 11,
            //        ProteinPer100 = 0.7f,
            //        FatsPer100 = 0.3f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Strawberries",
            //        ImageUrl = "img/fruits/Strawberries.jpg",
            //        CaloriesPer100 = 32,
            //        CarbsPer100 = 8,
            //        ProteinPer100 = 0.8f,
            //        FatsPer100 = 0.3f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Watermelon",
            //        ImageUrl = "img/fruits/Watermelon.jpg",
            //        CaloriesPer100 = 30,
            //        CarbsPer100 = 7.6f,
            //        ProteinPer100 = 0.6f,
            //        FatsPer100 = 0,
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 5,
            //        ImageUrl = "img/garnish/Garnish.jpg",
            //        Name = "Garnish",
            //        Products = new List<Product>
            //        {
            //            new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "BrownRice",
            //        ImageUrl = "img/garnish/BrownRice.jpg",
            //        CaloriesPer100 = 112,
            //        CarbsPer100 = 24,
            //        ProteinPer100 = 2.3f,
            //        FatsPer100 = 0.8f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Buckwheat",
            //        ImageUrl = "img/garnish/Buckwheat.jpg",
            //        CaloriesPer100 = 343,
            //        CarbsPer100 = 71.5f,
            //        ProteinPer100 = 13.3f,
            //        FatsPer100 = 3.4f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Cuscus",
            //        ImageUrl = "img/garnish/Cuscus.jpg",
            //        CaloriesPer100 = 112,
            //        CarbsPer100 = 23.2f,
            //        ProteinPer100 = 3.8f,
            //        FatsPer100 = 0.2f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "FrenchFries",
            //        ImageUrl = "img/garnish/FrenchFries.jpg",
            //        CaloriesPer100 = 274,
            //        CarbsPer100 = 36,
            //        ProteinPer100 = 3.5f,
            //        FatsPer100 = 14.6f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "JasmineRice",
            //        ImageUrl = "img/garnish/JasmineRice.jpg",
            //        CaloriesPer100 = 170,
            //        CarbsPer100 = 32,
            //        ProteinPer100 = 3.8f,
            //        FatsPer100 = 2.5f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "MashedPotato",
            //        ImageUrl = "img/garnish/mashedPotato.jpg",
            //        CaloriesPer100 = 100,
            //        CarbsPer100 = 15.7f,
            //        ProteinPer100 = 1.8f,
            //        FatsPer100 = 3.5f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Pasta",
            //        ImageUrl = "img/garnish/Pasta.jpg",
            //        CaloriesPer100 = 131,
            //        CarbsPer100 = 25,
            //        ProteinPer100 = 5.1f,
            //        FatsPer100 = 1,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "PotatoWages",
            //        ImageUrl = "img/garnish/PotatoWegdes.jpg",
            //        CaloriesPer100 = 93,
            //        CarbsPer100 = 21,
            //        ProteinPer100 = 2,
            //        FatsPer100 = 2,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Qinoa",
            //        ImageUrl = "img/garnish/Qinoa.jpeg",
            //        CaloriesPer100 = 374,
            //        CarbsPer100 = 69,
            //        ProteinPer100 = 13,
            //        FatsPer100 = 5.8f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "SweetPotato",
            //        ImageUrl = "img/garnish/SweetPotato.jpg",
            //        CaloriesPer100 = 90,
            //        CarbsPer100 = 21,
            //        ProteinPer100 = 2,
            //        FatsPer100 = 0.1f,
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "WhiteRice",
            //        ImageUrl = "img/garnish/WhiteRice.jpeg",
            //        CaloriesPer100 = 130,
            //        CarbsPer100 = 28,
            //        ProteinPer100 = 2.7f,
            //        FatsPer100 = 0.3f,
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 6,
            //        ImageUrl = "img/junkFood/JunkFood.jpg",
            //        Name = "JunkFood",
            //        Products = new List<Product>
            //        {
            //             new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Burger",
            //        ImageUrl = "img/junkFood/JunkFood.jpg",
            //        CaloriesPer100 = 540,
            //        CarbsPer100 = 45,
            //        ProteinPer100 = 25,
            //        FatsPer100 = 29
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "ChickenNuggets",
            //        ImageUrl = "img/junkFood/ChickenNuggets.jpg",
            //        CaloriesPer100 = 470,
            //        CarbsPer100 = 30,
            //        ProteinPer100 = 22,
            //        FatsPer100 = 30
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "HotDog",
            //        ImageUrl = "img/junkFood/HotDog.jpg",
            //        CaloriesPer100 = 290,
            //        CarbsPer100 = 4.2f,
            //        ProteinPer100 = 10,
            //        FatsPer100 = 26
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Pizza",
            //        ImageUrl = "img/junkFood/Pizza.jpg",
            //        CaloriesPer100 = 266,
            //        CarbsPer100 = 33,
            //        ProteinPer100 = 11,
            //        FatsPer100 = 10
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Shawarma",
            //        ImageUrl = "img/junkFood/Shawarma.jpg",
            //        CaloriesPer100 = 392,
            //        CarbsPer100 = 45.7f,
            //        ProteinPer100 = 32.3f,
            //        FatsPer100 = 10.6f
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 7,
            //        ImageUrl = "img/meat/Meat.jpg",
            //        Name = "Meat",
            //        Products = new List<Product>
            //        {
            //            new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Beef",
            //        ImageUrl = "img/meat/Beef.jpg",
            //        CaloriesPer100 = 250,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 26,
            //        FatsPer100 = 15
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Chicken",
            //        ImageUrl = "img/meat/Chicken.jpg",
            //        CaloriesPer100 = 165,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 31,
            //        FatsPer100 = 3.6f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Duck",
            //        ImageUrl = "img/meat/Duck.jpg",
            //        CaloriesPer100 = 201,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 23.5f,
            //        FatsPer100 = 11.2f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "GroundBeef",
            //        ImageUrl = "img/meat/GroundBeef.jpg",
            //        CaloriesPer100 = 322,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 14,
            //        FatsPer100 = 30
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "LambChops",
            //        ImageUrl = "img/meat/LambChops.jpg",
            //        CaloriesPer100 = 294,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 25,
            //        FatsPer100 = 21
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "MeatBalls",
            //        ImageUrl = "img/meat/MeatBalls.jpg",
            //        CaloriesPer100 = 197,
            //        CarbsPer100 = 8,
            //        ProteinPer100 = 21,
            //        FatsPer100 = 9
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "MincedPork",
            //        ImageUrl = "img/meat/MincedPork.jpg",
            //        CaloriesPer100 = 297,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 25.7f,
            //        FatsPer100 = 20.7f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "MincedTurkey",
            //        ImageUrl = "img/meat/MincedTurkey.jpg",
            //        CaloriesPer100 = 203,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 27,
            //        FatsPer100 = 10
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Pork",
            //        ImageUrl = "img/meat/Pork.jpg",
            //        CaloriesPer100 = 283,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 26.4f,
            //        FatsPer100 = 19
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "T-boneSteak",
            //        ImageUrl = "img/meat/T-boneSteak.jpg",
            //        CaloriesPer100 = 247,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 24,
            //        FatsPer100 = 16
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "WholeChicken",
            //        ImageUrl = "img/meat/WholeChicken.jpg",
            //        CaloriesPer100 = 216,
            //        CarbsPer100 = 0,
            //        ProteinPer100 = 17,
            //        FatsPer100 = 15.9f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Schnitzel",
            //        ImageUrl = "img/meat/Schnitzel.jpg",
            //        CaloriesPer100 = 297,
            //        CarbsPer100 = 16.3f,
            //        ProteinPer100 = 16,
            //        FatsPer100 = 18.8f
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 8,
            //        ImageUrl = "img/nuts/Nuts.jpg",
            //        Name = "Nuts",
            //        Products = new List<Product>
            //        {
            //            new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Almonds",
            //        ImageUrl = "img/nuts/Almonds.jpg",
            //        CaloriesPer100 = 579,
            //        CarbsPer100 = 21.5f,
            //        ProteinPer100 = 21.5f,
            //        FatsPer100 = 49.9f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "BrazilianNut",
            //        ImageUrl = "img/nuts/BrazilianNut.jpg",
            //        CaloriesPer100 = 656,
            //        CarbsPer100 = 12,
            //        ProteinPer100 = 14,
            //        FatsPer100 = 66
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Cashew",
            //        ImageUrl = "img/nuts/Cashew.jpg",
            //        CaloriesPer100 = 553,
            //        CarbsPer100 = 30.19f,
            //        ProteinPer100 = 18.22f,
            //        FatsPer100 = 43.85f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Peanuts",
            //        ImageUrl = "img/nuts/Peanuts.jpg",
            //        CaloriesPer100 = 567,
            //        CarbsPer100 = 16.13f,
            //        ProteinPer100 = 25.8f,
            //        FatsPer100 = 49.2f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Pistacheo",
            //        ImageUrl = "img/nuts/Pistacheo.jpg",
            //        CaloriesPer100 = 560,
            //        CarbsPer100 = 27.17f,
            //        ProteinPer100 = 20.16f,
            //        FatsPer100 = 45.32f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Walnuts",
            //        ImageUrl = "img/nuts/Walnuts.jpg",
            //        CaloriesPer100 = 654,
            //        CarbsPer100 = 13.7f,
            //        ProteinPer100 = 15.2f,
            //        FatsPer100 = 65.2f
            //    },
            //        }
            //    },
            //    new FoodType()
            //    {
            //        Id = 9,
            //        ImageUrl = "img/vegg/Veggies.jpg",
            //        Name = "Veggies",
            //        Products = new List<Product>
            //        {
            //              new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Brocoli",
            //        ImageUrl = "img/vegg/Brocoli.jpeg",
            //        CaloriesPer100 = 34,
            //        CarbsPer100 = 6.6f,
            //        ProteinPer100 = 2.8f,
            //        FatsPer100 = 0.37f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Carrot",
            //        ImageUrl = "img/vegg/Carrot.jpg",
            //        CaloriesPer100 = 41,
            //        CarbsPer100 = 10,
            //        ProteinPer100 = 1,
            //        FatsPer100 = 0.2f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Cucumber",
            //        ImageUrl = "img/vegg/Cucumber.jpg",
            //        CaloriesPer100 = 15,
            //        CarbsPer100 = 3.63f,
            //        ProteinPer100 = 0.65f,
            //        FatsPer100 = 0.11f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Olive",
            //        ImageUrl = "img/vegg/Olive.jpg",
            //        CaloriesPer100 = 115,
            //        CarbsPer100 = 6,
            //        ProteinPer100 = 0.8f,
            //        FatsPer100 = 11
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Paprika",
            //        ImageUrl = "img/vegg/Paprika.jpg",
            //        CaloriesPer100 = 31,
            //        CarbsPer100 = 6,
            //        ProteinPer100 = 1,
            //        FatsPer100 = 0.3f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "Tomato",
            //        ImageUrl = "img/vegg/Tomato.jpg",
            //        CaloriesPer100 = 18,
            //        CarbsPer100 = 3.9f,
            //        ProteinPer100 = 0.9f,
            //        FatsPer100 = 0.2f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "GreekSalad",
            //        ImageUrl = "img/vegg/GreekSalad.jpg",
            //        CaloriesPer100 = 101,
            //        CarbsPer100 = 3.2f,
            //        ProteinPer100 = 6.66f,
            //        FatsPer100 = 6.9f
            //    },
            //    new Product()
            //    {      Id = Guid.NewGuid(),
            //        Title = "ChickenCaesarSalad",
            //        ImageUrl = "img/vegg/CaesarSalad.jpg",
            //        CaloriesPer100 = 170,
            //        CarbsPer100 = 6.5f,
            //        ProteinPer100 = 5.0f,
            //        FatsPer100 = 14.1f
            //    }
            //        }
            //    }
            //};



            await _dbContext.MuscleGroups.AddRangeAsync(muscleGroups);
            await _dbContext.Exercises.AddRangeAsync(exercises);
            //await _dbContext.FoodTypes.AddRangeAsync(foodTypes);
            //await _dbContext.Products.AddRangeAsync(products);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
