using Application.Data;
using Application.Support.Interfaces;
using BuildingBlocks.CQRS;
using MediatR;

namespace Application.MonthlyStatistics.Queries.GetBestExerciseStats
{
    public record GetBestExerciseStatsQuery(int ExerciseId) : IQuery<GetBestExerciseStatsResult>;
    public record GetBestExerciseStatsResult(ExerciseStats ExerciseStats);
    public class GetBestExerciseStatsQueryHandler : IRequestHandler<GetBestExerciseStatsQuery, GetBestExerciseStatsResult>
    {
        private readonly IMonthlyStatisticsRepository _context;
        private readonly ICurrentUserService _currentUserService;
        public GetBestExerciseStatsQueryHandler(IMonthlyStatisticsRepository context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        //Dapper
        public async Task<GetBestExerciseStatsResult> Handle(GetBestExerciseStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var exerciseStats = new ExerciseStats();
            var bestExerciseStats = await _context.GetBestExerciseStats(request.ExerciseId, userId);

            if (bestExerciseStats == null)
            {
                return new GetBestExerciseStatsResult(exerciseStats);
            }

            return new GetBestExerciseStatsResult(bestExerciseStats);
        }




        ////EFCore
        //public async Task<GetBestExerciseStatsResult> Handle(GetBestExerciseStatsQuery request, CancellationToken cancellationToken)
        //{
        //    var userId = _currentUserService.UserId;

        //    if (userId == null)
        //    {
        //        throw new UnauthorizedAccessException("User is not authenticated.");
        //    }

        //    var exerciseStats = new ExerciseStats();

        //    var bestExerciseStats = await _context.ExerciseSets
        //        .AsNoTracking()
        //        .Include(t => t.Training)
        //        .Include(e => e.Exercise)
        //        .Where(es => es.ExerciseId == request.ExerciseId && es.Training.UserId == userId)
        //        .GroupBy(t => t.TrainingId)
        //        .Select(group => new
        //        {
        //            TrainingId = group.Key,
        //            MaxWeight = group.Max(t => t.Weight),
        //            MaxReps = group.Max(t => t.Reps),
        //            ExerciseName = group.First().Exercise.Name,
        //            Sets = group.Count(t => t.ExerciseId == request.ExerciseId)
        //        })
        //        .OrderByDescending(es => es.MaxWeight)
        //        .ThenByDescending(es => es.MaxReps)
        //        .FirstOrDefaultAsync(cancellationToken);

        //    if (bestExerciseStats == null)
        //    {
        //        return new GetBestExerciseStatsResult(exerciseStats);
        //    }

        //    var result = new ExerciseStats
        //    {
        //        ExerciseName = bestExerciseStats.ExerciseName,
        //        TrainingId = bestExerciseStats.TrainingId,
        //        Weight = bestExerciseStats.MaxWeight.ToString(),
        //        Reps = bestExerciseStats.MaxReps,
        //        Sets = bestExerciseStats.Sets
        //    };

        //    return new GetBestExerciseStatsResult(result);
        //}
    }
}
