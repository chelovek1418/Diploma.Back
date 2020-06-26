using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.IdentityServer.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Email length must be in the range of 2 to 30")]
        public string Username { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password length must be in the range of 6 to 30")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public IEnumerable<AuthenticationScheme> ExternalProviders { get; set; }

        public LoginViewModel()
        {
            ExternalProviders = new List<AuthenticationScheme>();
        }
    }
}