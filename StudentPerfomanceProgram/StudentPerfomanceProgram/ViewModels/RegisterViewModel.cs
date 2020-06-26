using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.IdentityServer.ViewModels
{
    public class RegisterViewModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length must be in the range of 6 to 30")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Email length must be in the range of 2 to 30")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}