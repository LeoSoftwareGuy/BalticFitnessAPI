using MediatR;

namespace Application.MonthlyStatistics.Queries.GetLatestExerciseStats
{
    public class GetLatestExerciseStatsQuery : IRequest<ExerciseStats>
    {
        public int ExerciseId { get; set; }
    }
}
