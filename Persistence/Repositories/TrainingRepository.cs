
using System.Text.Json;
using Application.Data;
using Application.Trainings.DTOs.Trainings;
using Dapper;
using Domain;
using Persistence.SqlDataBase;

namespace Persistence.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly TrainingsDbConnectionFactory _connectionFactory;
        public TrainingRepository(TrainingsDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<List<SortedByDayTraining>> GetAllTrainings(string userId)
        {
            using var connection = _connectionFactory.Create();
            try
            {
                string sql = """
                     WITH TrainingDetails AS (
                         SELECT 
                             t.id AS TrainingId,
                             t.Trained AS Trained,
                             t.UserId AS UserId,
                             es.id AS ExerciseSetId,
                             es.Reps AS Reps,
                             es.Weight AS Weight,
                             e.id AS ExerciseId,
                             e.Name AS ExerciseName,
                             mg.id AS MuscleGroupId,
                             mg.Name AS MuscleGroupName
                         FROM Trainings t
                         JOIN ExerciseSets es ON es.TrainingId = t.id
                         JOIN Exercises e ON e.id = es.ExerciseId
                         JOIN Muscles mg ON mg.id = e.MuscleGroupId
                         WHERE t.UserId = @UserId
                     ),
                     Aggregated AS (
                         SELECT 
                             CAST(Trained AS DATE) AS TrainingDate,
                             EXTRACT(YEAR FROM Trained) AS TrainedAtYear,
                             EXTRACT(MONTH FROM Trained) AS TrainedAtMonth,
                             EXTRACT(DAY FROM Trained) AS TrainedAtDay,
                             TO_CHAR(Trained, 'HH24:MI') AS TrainedAtTime,
                             MuscleGroupName,
                             ExerciseId,
                             ExerciseName,
                             json_agg(
                                 json_build_object(
                                     'ExerciseId', ExerciseId,
                                     'Reps', Reps,
                                     'Weight', Weight
                                 )
                             ) AS ExerciseSets
                         FROM TrainingDetails
                         GROUP BY Trained, MuscleGroupName, ExerciseId, ExerciseName
                     )
                     SELECT 
                         TrainingDate,
                         TrainedAtYear,
                         TrainedAtMonth,
                         TrainedAtDay,
                         TrainedAtTime,
                         MuscleGroupName,
                         json_agg(
                             json_build_object(
                                 'Name', ExerciseName,
                                 'Id', ExerciseId,
                                 'ExerciseSets', ExerciseSets
                             )
                         ) AS ExercisesPerMuscleGroup
                     FROM Aggregated
                     GROUP BY TrainingDate, TrainedAtYear, TrainedAtMonth, TrainedAtDay, TrainedAtTime, MuscleGroupName
                     ORDER BY TrainingDate DESC;
                     """;

                var rawResults = await connection.QueryAsync(sql, new { UserId = userId });

                var sortedByDayTrainings = rawResults
                    .GroupBy(r => new
                    {
                        r.trainingdate,
                        r.trainedatyear,
                        r.trainedatmonth,
                        r.trainedatday,
                        r.trainedattime
                    })
                    .Select(group => new SortedByDayTraining
                    {
                        TrainedAtDay = (int)group.Key.trainedatday,
                        TrainedAtMonth = (int)group.Key.trainedatmonth,
                        TrainedAtYear = (int)group.Key.trainedatyear,
                        TrainedAtTime = group.Key.trainedattime,
                        ExercisesPerMuscleGroup = group.ToDictionary(
                            g => (string)g.musclegroupname,
                            g => JsonSerializer.Deserialize<List<ExerciseGroupDto>>((string)g.exercisespermusclegroup)
                        )
                    })
                    .ToList();

                return sortedByDayTrainings;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve all trainings", ex);
            }
        }


        public async Task<int> SaveTraining(Training training)
        {
            using var connection = _connectionFactory.Create();
            using var transaction = connection.BeginTransaction();

            try
            {
                var trainingId = await connection.ExecuteScalarAsync<int>(@"
            INSERT INTO Trainings (UserId, Trained) 
            VALUES (@UserId, @Trained) 
            RETURNING Id;",
                    new { UserId = training.UserId, Trained = DateTime.UtcNow },
                    transaction);

                if (training.ExerciseSets?.Any() == true)
                {
                    var insertExerciseSetsQuery = @"
                INSERT INTO ExerciseSets (ExerciseId, Reps, Weight, TrainingId)
                VALUES (@ExerciseId, @Reps, @Weight, @TrainingId);";

                    var parameters = training.ExerciseSets.Select(set => new
                    {
                        set.ExerciseId,
                        set.Reps,
                        set.Weight,
                        TrainingId = trainingId
                    });

                    await connection.ExecuteAsync(insertExerciseSetsQuery, parameters, transaction);
                }

                transaction.Commit();
                return trainingId;
            }
            catch (Exception ex)
            {

                transaction.Rollback();
                throw;
            }
        }
    }
}
