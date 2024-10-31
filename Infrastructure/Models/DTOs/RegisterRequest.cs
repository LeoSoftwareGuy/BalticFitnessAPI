using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.DTOs
{
    public  class RegisterRequest
    {
        [Required(ErrorMessage = "User name is required.")]
        [StringLength(20, ErrorMessage = "First Name is too Long")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(20, ErrorMessage = "Last Name is too Long")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email name is invalid")]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

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
