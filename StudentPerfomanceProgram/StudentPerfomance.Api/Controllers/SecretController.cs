using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
