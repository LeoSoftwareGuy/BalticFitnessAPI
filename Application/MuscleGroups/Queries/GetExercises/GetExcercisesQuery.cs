using MediatR;

namespace Application.MuscleGroups.Queries.GetExercises
{
    public class GetExcercisesQuery : IRequest<List<ExerciseDto>>
    {
        public int MuscleGroupId { get; set; }
    }
}
