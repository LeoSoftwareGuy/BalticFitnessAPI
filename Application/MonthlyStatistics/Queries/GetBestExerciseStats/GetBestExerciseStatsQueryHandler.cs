using Application.Data;
using Application.Support.Interfaces;
using AutoMapper;
using BuildingBlocks.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MonthlyStatistics.Queries.GetBestExerciseStats
{
    public record GetBestExerciseStatsQuery(int ExerciseId) : IQuery<GetBestExerciseStatsResult>;
    public record GetBestExerciseStatsResult(ExerciseStats ExerciseStats);
    public class GetBestExerciseStatsQueryHandler : IRequestHandler<GetBestExerciseStatsQuery, GetBestExerciseStatsResult>
    {
        private readonly IMapper _mapper;
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetBestExerciseStatsQueryHandler(IMapper mapper, ITrainingDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<GetBestExerciseStatsResult> Handle(GetBestExerciseStatsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // SELET t.Id as TrainingId, MAX(es.Weight) as MaxWeight, MAX(es.Reps) as MaxReps, e.Name as ExerciseName,COUNT(*) AS Sets
            // FROM ExerciseSets es 
            // INNER JOIN Trainings t ON es.Training_Id = t.Id
            // INNER JOIN Exercises e ON es.Exercise_Id = e.Id
            // WHERE e.Id = request.ExerciseId AND t.UserId = userId
            // GROUP BY t.Id
            // ORDER BY MaxWeight DESC, MaxReps DESC
            // LIMIT 1;

            var bestExerciseStats = await _context.ExerciseSets
                .AsNoTracking()
                .Include(t => t.Training)
                .Include(e => e.Exercise)
                .Where(es => es.ExerciseId == request.ExerciseId && es.Training.UserId == userId)
                .GroupBy(t => t.TrainingId)
                .Select(group => new
                {
                    TrainingId = group.Key,
                    MaxWeight = group.Max(t => t.Weight),
                    MaxReps = group.Max(t => t.Reps),
                    ExerciseName = group.First().Exercise.Name,
                    Sets = group.Count(t => t.ExerciseId == request.ExerciseId)
                })
                .OrderByDescending(es => es.MaxWeight)
                .ThenByDescending(es => es.MaxReps)
                .FirstOrDefaultAsync(cancellationToken);

            if (bestExerciseStats == null)
            {
                throw new InvalidOperationException("No data found for the given exercise.");
            }

            return new GetBestExerciseStatsResult(new ExerciseStats
            {
                ExerciseName = bestExerciseStats.ExerciseName,
                TrainingId = bestExerciseStats.TrainingId,
                Weight = bestExerciseStats.MaxWeight.ToString(),
                Reps = bestExerciseStats.MaxReps,
                Sets = bestExerciseStats.Sets
            });
        }
    }
}
