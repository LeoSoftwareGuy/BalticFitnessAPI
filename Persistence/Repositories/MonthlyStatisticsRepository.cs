using System.Text.Json;
using Application.Data;
using Application.MonthlyStatistics;
using Application.Trainings.DTOs.Trainings;
using Dapper;
using Persistence.SqlDataBase;

namespace Persistence.Repositories
{
    public record BestWorkingWeightPerExercise(string ExerciseName, string BestWeight);
    public class AvgStats {

        public AvgStats()
        {
            
        }
        public double Reps { get; set; }
        public double Sets { get; set; }
    }


    public class MonthlyStatisticsRepository : IMonthlyStatisticsRepository
    {
        private readonly TrainingsDbConnectionFactory _connectionFactory;
        public MonthlyStatisticsRepository(TrainingsDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<ExerciseStats> GetBestExerciseStats(int exerciseId, string userId)
        {
            using var connection = _connectionFactory.Create();
            try
            {
                string sql = """
                             SELECT t.Id as TrainingId,
                             MAX(es.Weight) as Weight,
                             MAX(es.Reps) as Reps,
                             e.Name as ExerciseName,
                             COUNT(*) AS Sets
                             FROM ExerciseSets es 
                             INNER JOIN Trainings t ON es.trainingid = t.Id
                             INNER JOIN Exercises e ON es.exerciseid = e.Id
                             WHERE e.Id = @ExerciseId AND t.UserId = @UserId
                             GROUP BY t.Id,e.Name
                             ORDER BY Weight DESC, Reps DESC
                             LIMIT 1;
                             """;

                var exerciseStats = await connection.QueryFirstOrDefaultAsync<ExerciseStats>(sql,
                    new
                    {
                        ExerciseId = exerciseId,
                        UserId = userId
                    });
                return exerciseStats;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve the best exercise stats", ex);
            }
        }

        public async Task<ExerciseStatsLastThreeMonths> GetExerciseHistory(int exerciseId, string userId)
        {
            using var connection = _connectionFactory.Create();
            try
            {
                // Query for Exercise Name
                string sqlToGetExerciseName = """
                            SELECT e.name
                            FROM exercises e
                            WHERE e.Id = @Id
                            """;

                var sql = """
                  SELECT
                     T.TRAINED,
                     json_agg(json_build_object('Reps', ES.REPS, 'Weight', ES.WEIGHT,'ExerciseId', ES.EXERCISEID )) AS sets
                   FROM
                    TRAININGS T
                    JOIN
                        EXERCISESETS ES ON T.ID = ES.TRAININGID
                        WHERE
                        T.USERID = @UserId
                       AND ES.EXERCISEID = @ExerciseId
                       AND T.TRAINED >= (CURRENT_TIMESTAMP - INTERVAL '3 month')
                  GROUP BY
                    T.TRAINED
                 ORDER BY
                    T.TRAINED DESC;
                 """;

                // Execute and Map
                var result = await connection.QueryAsync(sql, new { UserId = userId, ExerciseId = exerciseId });
                var exerciseName = await connection.QueryFirstOrDefaultAsync<string>(
                    sqlToGetExerciseName,
                    new { Id = exerciseId }
                );

                var groupedHistory = result.ToDictionary(
                               r => (DateTime)r.trained,
                               r => JsonSerializer.Deserialize<List<ExerciseSetDto>>(((string)r.sets)) // Cast to string before deserialization
                             );


                // Return Final Mapped Object
                return new ExerciseStatsLastThreeMonths
                {
                    ExerciseHistory = groupedHistory,
                    ExerciseName = exerciseName ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve exercise stats for the last three months", ex);
            }
        }


        public async Task<ExerciseStats> GetLatestExerciseStats(int exerciseId, string userId)
        {
            using var connection = _connectionFactory.Create();
            try
            {
                string sql = """
                             
                             SELECT t.Id as TrainingId,
                             es.Weight as Weight, 
                             es.Reps as Reps,
                             e.Name as ExerciseName,
                             COUNT(*) as Sets
                             FROM ExerciseSets es
                             INNER JOIN Trainings t ON es.trainingid = t.Id
                             INNER JOIN Exercises e ON es.exerciseid = e.Id
                             WHERE e.Id = @ExerciseId AND t.UserId = @UserId
                             AND t.Trained = (
                               SELECT MAX(Trained)
                               FROM Trainings
                               WHERE UserId = @UserId
                             )
                             GROUP BY t.Id,e.Name,es.Weight,es.Reps
                             ORDER BY t.Trained DESC
                             LIMIT 1;
                             
                             """;

                var exerciseStats = await connection.QueryFirstOrDefaultAsync<ExerciseStats>(sql,
                    new
                    {
                        ExerciseId = exerciseId,
                        UserId = userId
                    });
                return exerciseStats;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve the best exercise stats", ex);
            }
        }

        public async Task<StatResults> GetMonthlyStats(string userId)
        {
            using var connection = _connectionFactory.Create();
            try
            {

                var bestWrokingWeightPerExercise = new { ExerciseName = string.Empty, BestWeight = string.Empty };
                string sqlForBestWorkingWeightPerExercise = """
                                                            SELECT
                                                            E.NAME AS EXERCISENAME,
                                                            MAX(ES.WEIGHT) AS BestWeight
                                                            FROM
                                                            TRAININGS T
                                                                JOIN EXERCISESETS ES ON ES.TRAININGID = T.ID
                                                                JOIN EXERCISES E ON E.ID = ES.EXERCISEID
                                                            WHERE
                                                                T.USERID = @UserId
                                                                AND T.TRAINED >= (CURRENT_TIMESTAMP - INTERVAL '1 month') --per month
                                                            GROUP BY
                                                                E.NAME;
                                                            
                                                            """;

                string sqlForAvgSetsAvgReps = """
                                             SELECT
                                             	(COUNT(*) / COUNT(DISTINCT T.ID)) AS AVGSETSPERTRAINING,
                                             	(SUM(ES.REPS) / COUNT(DISTINCT T.ID)) AS AVGREPSPERTRAINING
                                             FROM
                                             	EXERCISESETS ES
                                             	JOIN TRAININGS T ON ES.TRAININGID = T.ID
                                             WHERE
                                             	T.USERID = @UserId
                                             	AND T.TRAINED >= (CURRENT_TIMESTAMP - INTERVAL '1 month');
                                             """;

                var bestWorkingWeightPerExercise = await connection.QueryAsync<BestWorkingWeightPerExercise>(sqlForBestWorkingWeightPerExercise, new { UserId = userId });
                var avgResults = await connection.QuerySingleOrDefaultAsync<AvgStats>(sqlForAvgSetsAvgReps, new { UserId = userId });

                var statResults = new StatResults
                {
                    AverageAmountOfRepsPerTraining = (double)avgResults.Reps,
                    AverageAmountOfSetsPerTraining = (double)avgResults.Sets,
                    BestWorkingWeightPerExercise = bestWorkingWeightPerExercise.ToDictionary(g => g.ExerciseName, g => g.BestWeight)
                };

                return statResults;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve monthly stats", ex);
            }
        }

        public async Task<SummaryInfoBasedOnFilter> GetSummaryInfoBasedOnFilter(string filter, string userId)
        {
            using var connection = _connectionFactory.Create();
            try
            {

                string intervalCondition = filter switch
                {
                    "Week" => "AND TRAININGS.TRAINED >= (CURRENT_TIMESTAMP - INTERVAL '1 week')",
                    "Month" => "AND TRAININGS.TRAINED >= (CURRENT_TIMESTAMP - INTERVAL '1 month')",
                    "All" => ""  // No date filter for 'All'
                };

                string sql = $"""
          SELECT
              COALESCE(CAST(COUNT(DISTINCT TRAININGS.ID) AS INT), 0) AS SESSIONS,
              COALESCE(CAST(COUNT(*) AS INT), 0) AS SETS,
              COALESCE(CAST(COUNT(DISTINCT EXERCISES.MUSCLEGROUPID) AS INT), 0) AS MUSCLEGROUPS
          FROM
              EXERCISESETS
              JOIN TRAININGS ON EXERCISESETS.TRAININGID = TRAININGS.ID
              JOIN EXERCISES ON EXERCISES.ID = EXERCISESETS.EXERCISEID
          WHERE
              TRAININGS.USERID = @UserId
              {intervalCondition};
          """;

                var summaryInfoBasedOnFilter = await connection.QuerySingleOrDefaultAsync<SummaryInfoBasedOnFilter>(sql, new
                {
                    UserId = userId
                });

                return summaryInfoBasedOnFilter;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve summary info based on stats", ex);
            }
        }
    }
}
