using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.DTOs
{
    public  class RegisterRequest
    {
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(20, ErrorMessage = "First Name is too Long")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email name is invalid")]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
