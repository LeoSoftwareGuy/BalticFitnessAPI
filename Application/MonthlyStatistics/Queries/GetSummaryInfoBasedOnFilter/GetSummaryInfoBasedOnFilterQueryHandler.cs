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






        ////EFCore
        //public async Task<GetSummaryInfoBasedOnFilterResult> Handle(GetSummaryInfoBasedOnFilterQuery request, CancellationToken cancellationToken)
        //{
        //    var userId = _currentUserService.UserId;

        //    if (userId == null)
        //    {
        //        throw new UnauthorizedAccessException("User is not authenticated.");
        //    }

        //    var summaryInfo = new SummaryInfoBasedOnFilter();

        //    var trainings = await GetAllTrainings(userId, request.Filter);
        //    if (trainings.Count() > 0)
        //    {
        //        var sessionsCount = trainings.Count;
        //        var exercisesCount = trainings.Sum(t => t.ExerciseSets.Count);

        //        var muscleGroupsTrained = trainings
        //                .SelectMany(t => t.ExerciseSets)
        //                .Select(es => es.Exercise.MuscleGroupId) 
        //                .Distinct()                   
        //                .Count();

        //        summaryInfo.SessionsCount = sessionsCount;
        //        summaryInfo.ExercisesCount = exercisesCount;
        //        summaryInfo.MuscleGroupsCount = muscleGroupsTrained;
        //    }

        //    return new GetSummaryInfoBasedOnFilterResult(summaryInfo);
        //}



        //private async Task<List<Training>> GetAllTrainings(string userId, string filter)
        //{
        //    DateTime startDate = filter switch
        //    {
        //        "Week" => DateTime.UtcNow.AddDays(-7),       // Last 7 days in UTC
        //        "Month" => DateTime.UtcNow.AddMonths(-1),    // Last 30 days in UTC
        //        "All" => DateTime.MinValue,               // All data
        //        _ => throw new ArgumentException("Invalid filter value.")
        //    };

        //    var query = _context.Trainings
        //        .Where(t => t.UserId == userId);

        //    if (filter != "All")
        //    {
        //        query = query.Where(t => t.Trained >= startDate);
        //    }

        //    return await query
        //        .Include(t => t.ExerciseSets)
        //        .ThenInclude(es => es.Exercise)  // maybe I dont need this, check client.
        //        .AsNoTracking()
        //        .ToListAsync();
        //}
    }
}
