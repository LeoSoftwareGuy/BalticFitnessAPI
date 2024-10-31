using MediatR;

namespace Application.MuscleGroups.Queries.GetExercises
{
    public class GetExcercisesQuery : IRequest<List<ExerciseDto>>
    {
        public string BodyPartUrl { get; set; }
    }
}
