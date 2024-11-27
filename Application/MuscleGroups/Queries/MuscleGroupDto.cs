using Application.Support.Mappings;
using Domain;

namespace Application.MuscleGroups.Queries
{
    public class MuscleGroupDto : IMapFrom<MuscleGroup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public List<ExerciseDto> Exercises { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<MuscleGroup, MuscleGroupDto>();
        }
    }
}
