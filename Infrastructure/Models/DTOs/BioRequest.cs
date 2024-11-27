using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.DTOs
{
    public class BioRequest
    {
        [Required(ErrorMessage = "Nationality is Required")]
        [StringLength(50, ErrorMessage = "Nationality is too Long")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 200, ErrorMessage = "You do not fit into age range")]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
