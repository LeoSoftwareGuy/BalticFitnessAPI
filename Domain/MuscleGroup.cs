namespace Domain
{
    public class MuscleGroup
    {
        public MuscleGroup()
        {
            Exercises = new HashSet<Exercise>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public ICollection<Exercise> Exercises { get;  set; }
    }
}
