using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.IdentityServer.ViewModels
{
    public class ExternalRegisterViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Email length must be in the range of 2 to 30")]
        public string Username { get; set; }
        public string ReturnUrl { get; set; }
    }
}
