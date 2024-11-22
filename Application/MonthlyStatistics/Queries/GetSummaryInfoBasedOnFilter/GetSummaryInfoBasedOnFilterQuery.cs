using MediatR;

namespace Application.MonthlyStatistics.Queries.GetSummaryInfoBasedOnFilter
{
    public class GetSummaryInfoBasedOnFilterQuery : IRequest<SummaryInfoBasedOnFilter>
    {
        public string Filter { get; set; }
    }
}
