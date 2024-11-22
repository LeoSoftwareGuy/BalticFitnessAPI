using MediatR;

namespace Application.MonthlyStatistics.Queries.GetMonthlyStats
{
    public class GetMonthlyStatsQuery : IRequest<StatResults>
    {
    }
}
