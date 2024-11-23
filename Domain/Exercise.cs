namespace Domain
{
    public class Exercise
    {
        public Exercise()
        {
            ExerciseSets = new HashSet<ExerciseSet>();  
        }
        public int Id { get; set; }   
        public int MuscleGroupId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public MuscleGroup MuscleGroup { get; set; }
        public ICollection<ExerciseSet> ExerciseSets { get; set; }
    }
}
