using Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Ping()
        {
            IActionResult response = Ok("Pong.");
            return response;
        }
    }
}