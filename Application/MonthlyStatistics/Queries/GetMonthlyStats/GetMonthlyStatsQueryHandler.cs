using Application.Data;
using Application.Support.Interfaces;
using BuildingBlocks.CQRS;


namespace Application.MonthlyStatistics.Queries.GetMonthlyStats
{
    public record GetMonthlyStatsQuery : IQuery<GetMonthlyStatsResult>;
    public record GetMonthlyStatsResult(StatResults StatResults);

    public class GetMonthlyStatsQueryHandler : IQueryHandler<GetMonthlyStatsQuery, GetMonthlyStatsResult>
    {
        private readonly IMonthlyStatisticsRepository _context;
        private readonly ICurrentUserService _currentUserService;
        public GetMonthlyStatsQueryHandler(IMonthlyStatisticsRepository context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        //Dapper
        public async Task<GetMonthlyStatsResult> Handle(GetMonthlyStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var stats = new StatResults();

            var monthlyStats = await _context.GetMonthlyStats(userId);
            if (monthlyStats == null)
            {
                return new GetMonthlyStatsResult(stats);
            }
            return new GetMonthlyStatsResult(monthlyStats);
        }





        /// <summary>
        /// /EFcore
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //public async Task<GetMonthlyStatsResult> Handle(GetMonthlyStatsQuery request, CancellationToken cancellationToken)
        //{
        //    var userId = _currentUserService.UserId;

        //    if (userId == null)
        //    {
        //        throw new UnauthorizedAccessException("User is not authenticated.");
        //    }

        //    var stats = new StatResults();

        //    var bestWorkingWeightPerExercise = await GetBestWeightResultsPerExercise(userId);
        //    var (averageAmountOfRepsPerTraining, averageAmountOfSetsPerTraining) = await GetTrainingAverages(userId);

        //    stats.BestWorkingWeightPerExercise = bestWorkingWeightPerExercise;
        //    stats.AverageAmountOfRepsPerTraining = averageAmountOfRepsPerTraining;
        //    stats.AverageAmountOfSetsPerTraining = averageAmountOfSetsPerTraining;


        //    //var mealsPerMonth = await GetLast30DaysMeals(userId);

        //    //if (mealsPerMonth.Count > 0)
        //    //{
        //    //    stats.AverageAmountOfCaloriesPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfCaloriesPerDay(mealsPerMonth), 2);
        //    //    stats.AverageAmountOfProteinsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfProteinsPerDay(mealsPerMonth), 2);
        //    //    stats.AverageAmountOfFatsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfFatsPerDay(mealsPerMonth), 2);
        //    //    stats.AverageAmountOfCarbsPerDay = mealsPerMonth.Count.Equals(0) ? 0 : RoundUpForDouble(GetAverageAmountOfCarbsPerDay(mealsPerMonth), 2);
        //    //}

        //    return new GetMonthlyStatsResult(stats);
        //}


        //private async Task<Dictionary<string, string>> GetBestWeightResultsPerExercise(string userId)
        //{
        //    var bestWeightPerExercise = await _context.ExerciseSets
        //                      .AsNoTracking()
        //                      .Include(c => c.Training)
        //                      .Include(c => c.Exercise)
        //                      .Where(c => c.Training.UserId.Equals(userId) &&
        //                              c.Training.Trained >= DateTime.UtcNow.AddMonths(-1))
        //                     .OrderByDescending(c => c.Training.Trained)
        //                     .GroupBy(c => c.Exercise.Name)
        //                     .Select(g => new
        //                     {
        //                         ExerciseName = g.Key,
        //                         BestWeight = g.Max(d => d.Weight)
        //                     })
        //                     .ToDictionaryAsync(g => g.ExerciseName, g => g.BestWeight);

        //    if (bestWeightPerExercise == null)
        //    {
        //        bestWeightPerExercise = new Dictionary<string, string?>();
        //    }

        //    return bestWeightPerExercise;
        //}

        //private async Task<(double avgReps, double avgSets)> GetTrainingAverages(string userId)
        //{
        //    var trainings = await _context.Trainings
        //                          .AsNoTracking()
        //                          .Include(c => c.ExerciseSets)
        //                          .Where(c => c.UserId.Equals(userId) &&
        //                                  c.Trained >= DateTime.UtcNow.AddMonths(-1))
        //                          .ToListAsync();

        //    var amountOfTrainings = trainings.Count;
        //    var overallReps = trainings.Sum(t => t.ExerciseSets.Sum(es => es.Reps));
        //    var overallSets = trainings.Sum(t => t.ExerciseSets.Count);

        //    var avgReps = amountOfTrainings > 0 && overallReps > 0
        //        ? Math.Round((double)overallReps / amountOfTrainings, 1)
        //        : 0;

        //    var avgSets = amountOfTrainings > 0 && overallSets > 0
        //        ? Math.Round((double)overallSets / amountOfTrainings, 1)
        //        : 0;

        //    return (avgReps, avgSets);
        //}
    }
}
