namespace Application.Trainings.DTOs.Trainings
{
    /// <summary>
    /// Returns a list of SortedByDay objects, each representing a day of the year when the user trained,
    /// Each object contains the date of the training and dictionary where key is the muscle group and values is the list of unique exercises
    /// performed on that day. Each exercies has list of sets. Each set has weight, reps
    /// 
    /// </summary>

    public class SortedByDayTraining
    {
        // not sure why 
        public string TrainedAtTime { get; set; }
        public int TrainedAtMonth { get; set; }
        public int TrainedAtDay { get; set; }
        public int TrainedAtYear { get; set; }
        public Dictionary<string, List<ExerciseGroupDto>> ExercisesPerMuscleGroup { get; set; }

        public SortedByDayTraining()
        {
            ExercisesPerMuscleGroup = new Dictionary<string, List<ExerciseGroupDto>>();
        }
    }
}
