using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class MuscleGroup
    {
        public MuscleGroup()
        {
            Exercises = new HashSet<Exercise>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public ICollection<Exercise> Exercises { get;  set; }
    }
}
