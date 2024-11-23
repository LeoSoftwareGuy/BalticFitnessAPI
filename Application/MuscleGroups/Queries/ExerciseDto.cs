using Application.Support.Mappings;
using Domain;

namespace Application.MuscleGroups.Queries
{
    public class ExerciseDto : IMapFrom<Exercise>
    {
        public int Id { get; set; }
        public int MuscleGroupId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<Exercise, ExerciseDto>().ReverseMap();
        }
    }
}
