using Microsoft.AspNetCore.Mvc;

namespace OfferManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping() => Ok(true);
    }
}
