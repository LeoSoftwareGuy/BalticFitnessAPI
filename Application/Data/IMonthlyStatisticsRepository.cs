using Application.MonthlyStatistics;

namespace Application.Data
{
    public interface IMonthlyStatisticsRepository
    {
        Task<ExerciseStats> GetBestExerciseStats(int exerciseId, string userId);
        Task<ExerciseStatsLastThreeMonths> GetExerciseHistory(int exerciseId, string userId);
        Task<ExerciseStats> GetLatestExerciseStats(int exerciseId, string userId);
        Task<StatResults> GetMonthlyStats(string userId);
        Task<SummaryInfoBasedOnFilter> GetSummaryInfoBasedOnFilter(string filter, string userId);
    }
}
