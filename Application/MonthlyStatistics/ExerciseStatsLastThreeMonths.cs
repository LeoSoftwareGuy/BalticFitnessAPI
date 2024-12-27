using Application.Trainings.DTOs.Trainings;

namespace Application.MonthlyStatistics
{
    public class ExerciseStatsLastThreeMonths
    {
        public string ExerciseName { get; set; }
        public Dictionary<DateTime,List<ExerciseSetDto>> ExerciseHistory { get; set; }
    }
}
