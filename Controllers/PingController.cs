using Microsoft.AspNetCore.Mvc;

namespace British_Kingdom_back.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
