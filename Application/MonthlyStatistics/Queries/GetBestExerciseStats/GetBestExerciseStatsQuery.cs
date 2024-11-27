using MediatR;

namespace Application.MonthlyStatistics.Queries.GetBestExerciseStats
{
    public class GetBestExerciseStatsQuery : IRequest<ExerciseStats>
    {
        public int ExerciseId { get; set; }
    }
}
