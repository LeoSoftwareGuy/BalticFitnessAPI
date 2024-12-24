﻿using Application.Data;
using Application.Support.Interfaces;
using AutoMapper;
using Domain.Nutrition;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.MonthlyStatistics.Queries.GetMonthlyStats
{
    public class GetMonthlyStatsQueryHandler : IRequestHandler<GetMonthlyStatsQuery, StatResults>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetMonthlyStatsQueryHandler(ITrainingDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<StatResults> Handle(GetMonthlyStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var stats = new StatResults();

            //var bestWorkingWeightPerExercise = await GetBestWeightResultsPerExercise(userId);
            //stats.BestWorkingWeightPerExercise = bestWorkingWeightPerExercise;
            //stats.AverageAmountOfRepsPerTraining = trainingsPerMonth.Count.Equals(0)
            //    ? 0
            //        : RoundUp(GetAverageAmountOfRepsPerTraining(trainingsPerMonth), 2);
            //    stats.AverageAmountOfSetsPerTraining = trainingsPerMonth.Count.Equals(0) ? 0 : RoundUp(GetAverageAmountOfSetsPerTraining(trainingsPerMonth), 2);


            //var mealsPerMonth = await GetLast30DaysMeals(userId);

            //if (mealsPerMonth.Count > 0)
            //{
            //    stats.AverageAmountOfCaloriesPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfCaloriesPerDay(mealsPerMonth), 2);
            //    stats.AverageAmountOfProteinsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfProteinsPerDay(mealsPerMonth), 2);
            //    stats.AverageAmountOfFatsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfFatsPerDay(mealsPerMonth), 2);
            //    stats.AverageAmountOfCarbsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfCarbsPerDay(mealsPerMonth), 2);
            //}

            return stats;
        }


        private async Task<Dictionary<string, string>> GetBestWeightResultsPerExercise(string userId)
        {
           // SELECT e.Name AS ExerciseName, MAX(es.Weight) AS BestWeight
           // FROM ExerciseSets es
           // INNER JOIN Training t ON es.TrainingId = t.Id
           // INNER JOIN Exercise e ON e.Id = es.ExerciseId
           // WHERE t.UserId = @UserId
           //   AND t.Trained >= DATEADD(MONTH, -1, GETUTCDATE())-- Last month from current UTC time
           // GROUP BY e.Name
           //ORDER BY t.Trained DESC; --Ordering by the most recent training

            var bestWeightPerExercise = await _context.ExerciseSets
                              .AsNoTracking()
                              .Include(c => c.Training)
                              .Include(c => c.Exercise)
                              .Where(c => c.Training.UserId.Equals(userId) &&
                                      c.Training.Trained >= DateTime.UtcNow.AddMonths(-1))
                             .OrderByDescending(c => c.Training.Trained) 
                             .GroupBy(c => c.Exercise.Name)
                             .Select(g => new
                             {
                                   ExerciseName = g.Key,
                                   BestWeight = g.Max(d => d.Weight)
                             })
                             .ToDictionaryAsync(g => g.ExerciseName, g => g.BestWeight);  
      
            if (bestWeightPerExercise == null)
            {
                bestWeightPerExercise = new Dictionary<string, string?>();
            }

            return bestWeightPerExercise;
        }


        private async Task<List<Meal>> GetLast30DaysMeals(string userId)
        {
            var meals = await _context.Meals
               .Where(c => c.UserId.Equals(userId))
               .AsNoTracking()
               .ToListAsync();

            return meals;
        }








        //private Dictionary<string, double> BestWorkingWeightPerExercise(List<Training> trainingsPerMonth)
        //{
        //    var resultDict = new Dictionary<string, double>();
        //    foreach (var trainingDay in trainingsPerMonth.OrderByDescending(d => d.Trained))
        //    {
        //        foreach (var exerciseDto in trainingDay.ExerciseSets)
        //        {
        //            if (!resultDict.ContainsKey(exerciseDto.Exercise.Name))
        //                resultDict.Add(exerciseDto.Exercise.Name, 0);

        //            if (resultDict[exerciseDto.Exercise.Name] < exerciseDto.Weight)
        //                resultDict[exerciseDto.Exercise.Name] = exerciseDto.Weight;
        //        }
        //    }

        //    return resultDict;
        //}

        //private float GetAverageAmountOfRepsPerTraining(List<Training> trainingsPerMonth)
        //{
        //    var result = 0.0f;
        //    var amountOfTrainings = trainingsPerMonth.Count;
        //    var totalAmountOfRepsPerMonth = trainingsPerMonth.Sum(training => training.GetTrainingsOverallReps());

        //    result = totalAmountOfRepsPerMonth / amountOfTrainings;
        //    return result;
        //}

        //private float GetAverageAmountOfSetsPerTraining(List<Training> trainingsPerMonth)
        //{
        //    var amountOfTrainings = trainingsPerMonth.Count;
        //    var totalAmountOfRepsPerMonth = trainingsPerMonth.Sum(training => training.GetTrainingsOverallSets());

        //    var result = totalAmountOfRepsPerMonth / amountOfTrainings;
        //    return result;
        //}

        //private double GetAverageAmountOfCaloriesPerDay(List<Meal> meals)
        //{
        //    var amountOfMeals = meals.Count;
        //    var totalAmountOfCaloriesPerMonth = meals.Sum(training => training.GetMealsTotalCalories());
        //    var result = totalAmountOfCaloriesPerMonth / amountOfMeals;
        //    return result;
        //}

        //private double GetAverageAmountOfProteinsPerDay(List<Meal> trainingsPerMonth)
        //{
        //    var amountOfTrainings = trainingsPerMonth.Count;
        //    var totalAmountOfProteinPerMonth = trainingsPerMonth.Sum(training => training.GetMealsTotalProtein());
        //    var result = totalAmountOfProteinPerMonth / amountOfTrainings;
        //    return result;
        //}

        //private double GetAverageAmountOfFatsPerDay(List<Meal> trainingsPerMonth)
        //{
        //    var amountOfTrainings = trainingsPerMonth.Count;
        //    var totalAmountOfFatsPerMonth = trainingsPerMonth.Sum(training => training.GetMealsTotalFats());
        //    var result = totalAmountOfFatsPerMonth / amountOfTrainings;
        //    return result;
        //}

        //private double GetAverageAmountOfCarbsPerDay(List<Meal> trainingsPerMonth)
        //{
        //    var amountOfTrainings = trainingsPerMonth.Count;
        //    var totalAmountOfFatsPerMonth = trainingsPerMonth.Sum(training => training.GetMealsTotalCarbs());
        //    var result = totalAmountOfFatsPerMonth / amountOfTrainings;
        //    return result;
        //}
        //private double RoundUp(float input, int places)
        //{
        //    double multiplier = Math.Pow(10, Convert.ToDouble(places));
        //    return Math.Ceiling(input * multiplier) / multiplier;
        //}

        //private double RoundUpForDouble(double input, int places)
        //{
        //    double multiplier = Math.Pow(10, Convert.ToDouble(places));
        //    return Math.Ceiling(input * multiplier) / multiplier;
        //}
    }
}
