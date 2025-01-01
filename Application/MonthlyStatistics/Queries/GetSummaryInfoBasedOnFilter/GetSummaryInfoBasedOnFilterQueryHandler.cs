using Application.Data;
using Application.Support.Interfaces;
using BuildingBlocks.CQRS;


namespace Application.MonthlyStatistics.Queries.GetSummaryInfoBasedOnFilter
{
    public record GetSummaryInfoBasedOnFilterQuery(string Filter) : IQuery<GetSummaryInfoBasedOnFilterResult>;
    public record GetSummaryInfoBasedOnFilterResult(SummaryInfoBasedOnFilter SummaryInfoBasedOnFilter);
    public class GetSummaryInfoBasedOnFilterQueryHandler : IQueryHandler<GetSummaryInfoBasedOnFilterQuery, GetSummaryInfoBasedOnFilterResult>
    {
        private readonly IMonthlyStatisticsRepository _context;
        private readonly ICurrentUserService _currentUserService;
        public GetSummaryInfoBasedOnFilterQueryHandler(IMonthlyStatisticsRepository context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        //Dapper
        public async Task<GetSummaryInfoBasedOnFilterResult> Handle(GetSummaryInfoBasedOnFilterQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var summaryInfo = new SummaryInfoBasedOnFilter();

            var infoBasedOnFilter = await _context.GetSummaryInfoBasedOnFilter(request.Filter, userId);

            if (infoBasedOnFilter == null)
            {
                return new GetSummaryInfoBasedOnFilterResult(summaryInfo);
            }

            return new GetSummaryInfoBasedOnFilterResult(infoBasedOnFilter);
        }
    }
}
