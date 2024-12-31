using Application.Data;
using Dapper;
using Domain;
using Persistence.SqlDataBase;

namespace Persistence.Repositories
{
    public class MuscleGroupRepository : IMuscleGroupRepository
    {
        private readonly TrainingsDbConnectionFactory _connectionFactory;

        public MuscleGroupRepository(TrainingsDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<MuscleGroup>> GetAllMuscleGroups()
        {
            using var connection = _connectionFactory.Create();
            try
            {
                string sql = "SELECT * FROM muscles";
                return await connection.QueryAsync<MuscleGroup>(sql);
            }
            catch (Exception ex)
            {
                // Log error (consider using ILogger)
                throw new ApplicationException($"Failed to retrieve muscle groups", ex);
            }
           
        }

        public async Task<IEnumerable<MuscleGroup>> GetAllMuscleGroupsWithExercises()
        {
            using var connection = _connectionFactory.Create();

            try
            {
                string sql = """
                SELECT m.*, e.*
                FROM muscles m
                LEFT JOIN exercises e ON m.id = e.musclegroupid
                """;

                var muscleDictionary = new Dictionary<int, MuscleGroup>();

                var muscleGroups = await connection.QueryAsync<MuscleGroup, Exercise, MuscleGroup>(
                    sql,
                    (muscle, exercise) =>
                    {
                        if (!muscleDictionary.TryGetValue(muscle.Id, out var muscleEntry))
                        {
                            muscleEntry = muscle;
                            muscleEntry.Exercises = new List<Exercise>();
                            muscleDictionary[muscle.Id] = muscleEntry;
                        }
                        muscleEntry.Exercises.Add(exercise);
                        return muscleEntry;
                    },
                    splitOn: "id"); //This tells Dapper where to split the result set between MuscleGroup and Exercise
                                    //id here refers to the exercise's id because it's the first differing column after muscles.*
                                    // Without splitOn, Dapper won't know how to separate muscle group and exercise columns
                return muscleGroups.Distinct();
            }
            catch (Exception ex)
            {
                // Log error (consider using ILogger)
                throw new ApplicationException($"Failed to retrieve muscle groups with Exersises", ex);
            }
        }


        public async Task<MuscleGroup> GetMuscleGroup(int id)
        {
            using var connection = _connectionFactory.Create();

            try
            {
                string sql = """
                             select * from muscles
                             join exercises on muscles.id = exercises.musclegroupid
                             where muscles.id = @MuscleId
                             """;

                var muscleGroupDictionary = new Dictionary<int, MuscleGroup>();

                await connection.QueryAsync<MuscleGroup, Exercise, MuscleGroup>(
                    sql,
                    (muscle, exercise) =>
                    {
                        if (!muscleGroupDictionary.TryGetValue(muscle.Id, out var muscleGroup))
                        {
                            muscleGroup = muscle;
                            muscleGroup.Exercises = new List<Exercise>();
                            muscleGroupDictionary.Add(muscle.Id, muscleGroup);
                        }
                        muscleGroup.Exercises.Add(exercise);
                        return muscleGroup;
                    },
                    new { MuscleId = id }
                );
                return muscleGroupDictionary.Values.FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                // Log error (consider using ILogger)
                throw new ApplicationException($"Failed to retrieve muscle group with ID {id}", ex);
            }
        }
    }
}
