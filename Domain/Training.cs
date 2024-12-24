namespace Domain
{
    public class Training
    {
        public Training()
        {
            ExerciseSets = new HashSet<ExerciseSet>();
            Trained = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Trained { get; set; }

        public ICollection<ExerciseSet> ExerciseSets { get;  set; }
    }
}
