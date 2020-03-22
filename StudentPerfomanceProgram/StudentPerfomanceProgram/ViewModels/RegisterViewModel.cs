using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.IdentityServer.ViewModels
{
    public class RegisterViewModel
    {
        public string ReturnUrl { get; set; }
        //public string Username { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}