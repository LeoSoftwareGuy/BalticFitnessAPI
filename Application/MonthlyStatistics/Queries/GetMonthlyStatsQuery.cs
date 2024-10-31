using MediatR;

namespace Application.MonthlyStatistics.Queries
{
    public class GetMonthlyStatsQuery : IRequest<StatResults>
    {
    }
}
