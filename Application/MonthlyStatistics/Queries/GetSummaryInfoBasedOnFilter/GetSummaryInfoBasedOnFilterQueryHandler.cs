﻿using Application.Data;
using Application.Support.Interfaces;
using AutoMapper;
using BuildingBlocks.CQRS;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace Application.MonthlyStatistics.Queries.GetSummaryInfoBasedOnFilter
{
    public record GetSummaryInfoBasedOnFilterQuery(string Filter) : IQuery<GetSummaryInfoBasedOnFilterResult>;
    public record GetSummaryInfoBasedOnFilterResult(SummaryInfoBasedOnFilter SummaryInfoBasedOnFilter);
    public class GetSummaryInfoBasedOnFilterQueryHandler : IQueryHandler<GetSummaryInfoBasedOnFilterQuery, GetSummaryInfoBasedOnFilterResult>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetSummaryInfoBasedOnFilterQueryHandler(ITrainingDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<GetSummaryInfoBasedOnFilterResult> Handle(GetSummaryInfoBasedOnFilterQuery request, CancellationToken cancellationToken)
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


                //SELECT COUNT(DISTINCT e.MuscleGroupId)
                //FROM Trainings t
                //INNER JOIN ExerciseSets es ON t.Id = es.Training_Id
                //INNER JOIN Exercises e ON es.Exercise_Id = e.Id;

                var muscleGroupsTrained = trainings
                        .SelectMany(t => t.ExerciseSets)
                        .Select(es => es.Exercise.MuscleGroupId) 
                        .Distinct()                   
                        .Count();

                summaryInfo.SessionsCount = sessionsCount;
                summaryInfo.ExercisesCount = exercisesCount;
                summaryInfo.MuscleGroupsCount = muscleGroupsTrained;
            }

            return new GetSummaryInfoBasedOnFilterResult(summaryInfo);
        }



        private async Task<List<Training>> GetAllTrainings(string userId, string filter)
        {
            DateTime startDate = filter switch
            {
                "Week" => DateTime.Now.AddDays(-7),       // Last 7 days
                "Month" => DateTime.Now.AddMonths(-1),    // Last 30 days
                "All" => DateTime.MinValue,               // All data
                _ => throw new ArgumentException("Invalid filter value.")
            };

            var query = _context.Trainings
                .Where(t => t.UserId == userId);

            if (filter != "All")
            {
                query = query.Where(t => t.Trained >= startDate);
            }

            return await query
                .Include(t => t.ExerciseSets) 
                .ThenInclude(es => es.Exercise)  // maybe I dont need this, check client.
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
