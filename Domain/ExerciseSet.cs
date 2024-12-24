
namespace Domain
{
    public class ExerciseSet
    {
        public int Id { get; set; }
        public int Training_Id { get; set; }    
        public int Exercise_Id { get; set; }    
        public int Reps { get; set; }
        public string Weight { get; set; }

        public Exercise Exercise { get; set; }
        public Training Training { get; set; }
    }
}
