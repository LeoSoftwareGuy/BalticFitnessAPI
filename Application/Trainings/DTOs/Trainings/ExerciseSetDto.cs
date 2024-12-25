using Application.MuscleGroups.Queries;
using Application.Support.Mappings;
using AutoMapper;
using Domain;

namespace Application.Trainings.DTOs.Trainings
{
    /// <summary>
    /// Class represents a single set of an exercise
    /// </summary>
    public class ExerciseSetDto : IMapFrom<ExerciseSet>
    {
        public int ExerciseId { get; set; } 
        public int Reps { get; set; }
        public string Weight { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExerciseSet, ExerciseSetDto>().ReverseMap(); // This enables mapping in both directions;
        }
    }
}
