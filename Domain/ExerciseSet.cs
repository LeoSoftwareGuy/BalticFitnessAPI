namespace Domain
{
    public class ExerciseSet
    {
        public int Reps { get; set; }
        public double Weight { get; set; }
        public int Pre { get; set; }

        public Exercise Exercise { get; set; }
    }
}
