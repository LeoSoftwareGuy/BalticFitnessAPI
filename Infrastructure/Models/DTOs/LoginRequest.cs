using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email name is invalid")]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }
    }
}
