#nullable disable

using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Api.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords dont't match")]
        public string ConfirmPassword { get; set; }
    }
}