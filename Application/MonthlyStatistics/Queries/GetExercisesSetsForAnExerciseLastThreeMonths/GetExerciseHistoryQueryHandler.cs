using Application.Data;
using Application.Support.Interfaces;
using Application.Trainings.DTOs.Trainings;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;

namespace Application.MonthlyStatistics.Queries.GetExercisesSetsForAnExerciseLastThreeMonths
{
    public record GetExerciseHistoryQuery(int exerciseId) : IQuery<ExerciseStatsLastThreeMonths>;
    public class GetExerciseHistoryQueryHandler : IQueryHandler<GetExerciseHistoryQuery, ExerciseStatsLastThreeMonths>
    {
        private readonly ITrainingDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetExerciseHistoryQueryHandler(ITrainingDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<ExerciseStatsLastThreeMonths> Handle(GetExerciseHistoryQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var exerciseHistory = await _context.ExerciseSets
                .Include(es => es.Training)
                .AsNoTracking()
                .Where(es => es.ExerciseId == request.exerciseId
                             && es.Training.UserId == userId
                             && es.Training.Trained >= DateTime.UtcNow.AddMonths(-3))
                .GroupBy(es => es.Training.Trained)
                .Select(g => new
                {
                    TrainedAt = g.Key,
                    Sets = g.Select(es => new ExerciseSetDto
                    {
                        ExerciseId = es.ExerciseId,
                        Reps = es.Reps,
                        Weight = es.Weight
                    }).ToList()
                })
                .OrderByDescending(x => x.TrainedAt)
                .ToListAsync(cancellationToken);

            var exercise = await _context.Exercises.FirstOrDefaultAsync(c => c.Id.Equals(request.exerciseId));


            return new ExerciseStatsLastThreeMonths
            {
                ExerciseHistory = exerciseHistory.ToDictionary(x => x.TrainedAt, x => x.Sets),
                ExerciseName = exercise?.Name is null ? string.Empty : exercise.Name,
            };
        }
    }
}
