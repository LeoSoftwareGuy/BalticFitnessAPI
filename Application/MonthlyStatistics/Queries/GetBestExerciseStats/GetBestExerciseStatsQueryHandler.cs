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
    }
}
