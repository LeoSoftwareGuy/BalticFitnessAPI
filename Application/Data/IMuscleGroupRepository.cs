using Domain;

namespace Application.Data
{
    public interface IMuscleGroupRepository
    {
        Task<MuscleGroup> GetMuscleGroup(int id);
        Task<IEnumerable<MuscleGroup>> GetAllMuscleGroups();
        Task<IEnumerable<MuscleGroup>> GetAllMuscleGroupsWithExercises();
    }
}
