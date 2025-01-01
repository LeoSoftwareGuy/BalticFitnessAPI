using Application.Data;
using Application.Support.Interfaces;
using BuildingBlocks.CQRS;
using MediatR;


namespace Application.MonthlyStatistics.Queries.GetLatestExerciseStats
{
    public record GetLatestExerciseStatsQuery(int ExerciseId) : IQuery<GetLatestExerciseStatsResult>;
    public record GetLatestExerciseStatsResult(ExerciseStats ExerciseStats);
    public class GetLatestExerciseStatsQueryHandler : IRequestHandler<GetLatestExerciseStatsQuery, GetLatestExerciseStatsResult>
    {
        private readonly IMonthlyStatisticsRepository _context;
        private readonly ICurrentUserService _currentUserService;

        public GetLatestExerciseStatsQueryHandler(IMonthlyStatisticsRepository context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }



        //Dapper
        public async Task<GetLatestExerciseStatsResult> Handle(GetLatestExerciseStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var exerciseStats = new ExerciseStats();

            var latestExerciseStats = await _context.GetLatestExerciseStats(request.ExerciseId, userId);


            if (latestExerciseStats == null)
            {
                return new GetLatestExerciseStatsResult(exerciseStats);
            }

            return new GetLatestExerciseStatsResult(latestExerciseStats);
        }
    }
}
