using Application.Data;
using Application.Support.Interfaces;
using BuildingBlocks.CQRS;

namespace Application.MonthlyStatistics.Queries.GetExercisesSetsForAnExerciseLastThreeMonths
{
    public record GetExerciseHistoryQuery(int exerciseId) : IQuery<ExerciseStatsLastThreeMonths>;
    public class GetExerciseHistoryQueryHandler : IQueryHandler<GetExerciseHistoryQuery, ExerciseStatsLastThreeMonths>
    {
        private readonly IMonthlyStatisticsRepository _context;
        private readonly ICurrentUserService _currentUserService;
        public GetExerciseHistoryQueryHandler(IMonthlyStatisticsRepository context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }


        //Dapper
        public async Task<ExerciseStatsLastThreeMonths> Handle(GetExerciseHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var history = new ExerciseStatsLastThreeMonths();

            var exerciseHistory = await _context.GetExerciseHistory(request.exerciseId, userId);

            if (exerciseHistory == null)
            {
                return history;
            }

            return exerciseHistory;
        }
    }
}
