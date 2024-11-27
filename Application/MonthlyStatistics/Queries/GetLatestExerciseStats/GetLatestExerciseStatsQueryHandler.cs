using Application.Support.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using System.Collections.Generic;


namespace Application.MonthlyStatistics.Queries.GetLatestExerciseStats
{
    public class GetLatestExerciseStatsQueryHandler : IRequestHandler<GetLatestExerciseStatsQuery, ExerciseStats>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetLatestExerciseStatsQueryHandler(IMapper mapper, ITrainingDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ExerciseStats> Handle(GetLatestExerciseStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }


            // SELECT es.Training_Id, es.Weight, es.Reps, e.Name as ExerciseName
            // FROM ExerciseSets es
            // INNER JOIN Trainings t ON es.Training_Id = t.Id
            // INNER JOIN Exercises e ON es.Exercise_Id = e.Id
            // WHERE t.UserId = userId AND es.Exercise_Id = request.ExerciseId
            // ORDER BY t.Trained DESC
            // LIMIT 1;
             

            var latestExercuseStats = await _context.ExerciseSets
                .Include(es => es.Training)
                .Include(es => es.Exercise)
                .Where(es => es.Training.UserId == userId && es.Exercise_Id == request.ExerciseId)
                .OrderByDescending(es => es.Training.Trained)
                .FirstOrDefaultAsync(cancellationToken);

            if (latestExercuseStats == null)
            {
                throw new InvalidOperationException("No data found for the given exercise.");
            }

            return new ExerciseStats
            {
                ExerciseName = latestExercuseStats.Exercise.Name,
                TrainingId = latestExercuseStats.Training.Id,
                Weight = latestExercuseStats.Weight.ToString(),
                Sets= latestExercuseStats.
                Reps = latestExercuseStats.Reps
            };

        }
    }
}
