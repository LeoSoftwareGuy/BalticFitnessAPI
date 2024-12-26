using Application.Data;
using Application.Support.Interfaces;
using AutoMapper;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;


namespace Application.MonthlyStatistics.Queries.GetMonthlyStats
{
    public record GetMonthlyStatsQuery : IQuery<GetMonthlyStatsResult>;
    public record GetMonthlyStatsResult(StatResults StatResults);

    public class GetMonthlyStatsQueryHandler : IQueryHandler<GetMonthlyStatsQuery, GetMonthlyStatsResult>
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

        public async Task<GetMonthlyStatsResult> Handle(GetMonthlyStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var stats = new StatResults();

            var bestWorkingWeightPerExercise = await GetBestWeightResultsPerExercise(userId);
            var (averageAmountOfRepsPerTraining, averageAmountOfSetsPerTraining) = await GetTrainingAverages(userId);

            stats.BestWorkingWeightPerExercise = bestWorkingWeightPerExercise;
            stats.AverageAmountOfRepsPerTraining = averageAmountOfRepsPerTraining;
            stats.AverageAmountOfSetsPerTraining = averageAmountOfSetsPerTraining;


            //var mealsPerMonth = await GetLast30DaysMeals(userId);

            //if (mealsPerMonth.Count > 0)
            //{
            //    stats.AverageAmountOfCaloriesPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfCaloriesPerDay(mealsPerMonth), 2);
            //    stats.AverageAmountOfProteinsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfProteinsPerDay(mealsPerMonth), 2);
            //    stats.AverageAmountOfFatsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfFatsPerDay(mealsPerMonth), 2);
            //    stats.AverageAmountOfCarbsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfCarbsPerDay(mealsPerMonth), 2);
            //}

            return new GetMonthlyStatsResult(stats);
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

        private async Task<(double avgReps, double avgSets)> GetTrainingAverages(string userId)
        {
            //      SELECT
            //     (COUNT(*) / COUNT(DISTINCT t.id)) as avgSetsPerTraining,
            //	   (sum(es.reps) / COUNT(DISTINCT t.id)) as avgRepsPerTraining
            //     FROM
            //    EXERCISESETS ES
            //    JOIN TRAININGS T ON ES.TRAININGID = T.ID
            //    WHERE

            //    T.USERID = PARAMUSERID

            //    AND T.TRAINED >= (CURRENT_TIMESTAMP - INTERVAL '1 month');
            var trainings = await _context.Trainings
                                  .AsNoTracking()
                                  .Include(c => c.ExerciseSets)
                                  .Where(c => c.UserId.Equals(userId) &&
                                          c.Trained >= DateTime.UtcNow.AddMonths(-1))
                                  .ToListAsync();

            var amountOfTrainings = trainings.Count;
            var overallReps = trainings.Sum(t => t.ExerciseSets.Sum(es => es.Reps));
            var overallSets = trainings.Sum(t => t.ExerciseSets.Count);

            var avgReps = amountOfTrainings > 0 && overallReps > 0
                ? Math.Round((double)overallReps / amountOfTrainings, 1)
                : 0;

            var avgSets = amountOfTrainings > 0 && overallSets > 0
                ? Math.Round((double)overallSets / amountOfTrainings, 1)
                : 0;

            return (avgReps, avgSets);
        }



        //private async Task<List<Meal>> GetLast30DaysMeals(string userId)
        //{
        //    var meals = await _context.Meals
        //       .Where(c => c.UserId.Equals(userId))
        //       .AsNoTracking()
        //       .ToListAsync();

        //    return meals;
        //}








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
