using Application.Support.Mappings;
using AutoMapper;
using Domain;

namespace Application.Trainings.DTOs.Trainings
{
    public class TrainingDto : IMapFrom<Training>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime Trained { get; set; }

        public List<ExerciseSetDto> ExerciseSets { get; private set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Training, TrainingDto>().ReverseMap();
        }
    }
}
