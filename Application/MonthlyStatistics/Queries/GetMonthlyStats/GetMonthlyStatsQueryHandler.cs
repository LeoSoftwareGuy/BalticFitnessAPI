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
    }
}
