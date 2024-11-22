using Application.Support.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using MongoDB.Driver;
using Persistence.Interfaces;

namespace Application.MonthlyStatistics.Queries.GetSummaryInfoBasedOnFilter
{
    public class GetSummaryInfoBasedOnFilterQueryHandler : IRequestHandler<GetSummaryInfoBasedOnFilterQuery, SummaryInfoBasedOnFilter>
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetSummaryInfoBasedOnFilterQueryHandler(IMongoDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<SummaryInfoBasedOnFilter> Handle(GetSummaryInfoBasedOnFilterQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var summaryInfo = new SummaryInfoBasedOnFilter();

            var trainings = await GetAllTrainings(userId, request.Filter);
            if (trainings.Count() > 0)
            {
                var sessionsCount = trainings.Count;
                var exercisesCount = trainings.Sum(t => t.ExerciseSets.Count);
                var muscleGroupsTrained = trainings
                        .SelectMany(t => t.ExerciseSets)
                        .Select(es => es.Exercise.MuscleGroupId) 
                        .Distinct()                   
                        .Count();

                summaryInfo.SessionsCount = sessionsCount;
                summaryInfo.ExercisesCount = exercisesCount;
                summaryInfo.MuscleGroupsCount = muscleGroupsTrained;
            }

            return summaryInfo;
        }



        private async Task<List<Training>> GetAllTrainings(string userId, string filter)
        {
            var filterDefinition = Builders<Training>.Filter.Eq(c => c.UserId, userId);

            DateTime startDate = filter switch
            {
                "Week" => DateTime.Now.AddDays(-7),           // Last 7 days
                "Month" => DateTime.Now.AddMonths(-1),        // Last 30 days
                "All" => DateTime.MinValue,                   // All data
                _ => throw new ArgumentException("Invalid filter value.")
            };

            if (filter != "All")
            {
                // Add date range to the filter for "Week" or "Month"
                filterDefinition &= Builders<Training>.Filter.Gte(c => c.Trained, startDate);
            }

            // Execute the query with the applied filter
            var trainings = await _context.Trainings
                .Find(filterDefinition)
                .ToListAsync();

            return trainings;
        }
    }
}
