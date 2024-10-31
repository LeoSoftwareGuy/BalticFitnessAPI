namespace Domain
{
    public class Exercise
    {
        public Guid Id { get; set; }
       
        public int MuscleGroupId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
