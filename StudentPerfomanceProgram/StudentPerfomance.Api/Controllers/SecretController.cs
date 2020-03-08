using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StudentPerfomance.Api.Controllers
{
    [ApiController]
    public class SecretController : ControllerBase
    {
        public SecretController()
        {
        }

        [Route("/secret")]
        [Authorize]
        public string Index()
        {
            return "secret message lul";
        }
    }
}
