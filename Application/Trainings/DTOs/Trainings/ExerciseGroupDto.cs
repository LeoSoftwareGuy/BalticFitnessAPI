namespace Application.Trainings.DTOs.Trainings
{
    /// <summary>
    /// Class represents all sets for a particular exercise
    /// </summary>
    public class ExerciseGroupDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<ExerciseSetDto> ExerciseSets { get; set; }

        public ExerciseGroupDto()
        {
            ExerciseSets = new List<ExerciseSetDto>();
        }
    }
}
