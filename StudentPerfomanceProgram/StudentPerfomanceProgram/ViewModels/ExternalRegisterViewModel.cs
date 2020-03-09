using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.IdentityServer.Controllers
{
    public class ExternalRegisterViewModel
    {
        public string Username { get; set; }
        public string ReturnUrl { get; set; }
    }
}
