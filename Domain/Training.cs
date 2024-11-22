namespace Domain
{
    public class Training
    {
        public Training()
        {
            ExerciseSets = new HashSet<ExerciseSet>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Trained { get; set; }

        public ICollection<ExerciseSet> ExerciseSets { get;  set; }

        public float GetTrainingsOverallReps() => ExerciseSets.Sum(t => t.Reps);
        public float GetTrainingsOverallSets() => ExerciseSets.Count();
    }
}
