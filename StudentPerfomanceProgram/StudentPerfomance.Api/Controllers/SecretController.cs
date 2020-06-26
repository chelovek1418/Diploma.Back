using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace StudentPerfomance.Api.Controllers
{
    [ApiController]
    public class SecretController : ControllerBase
    {
        public SecretController()
        {
        }

        [Route("/secret")]
        [Authorize(Roles = "admin")]
        public string Index()
        {
            var kek = User.Claims.ToList();
            return "secret message lul";
        }
    }
}
