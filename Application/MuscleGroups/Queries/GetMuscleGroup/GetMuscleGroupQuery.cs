using MediatR;

namespace Application.MuscleGroups.Queries.GetMuscleGroup
{
    public class GetMuscleGroupQuery : IRequest<MuscleGroupDto>
    {
        public int Id { get; set; }
    }
}
