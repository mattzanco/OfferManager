using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OfferManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var email = User.FindFirstValue("preferred_username")
            ?? User.FindFirstValue(ClaimTypes.Email)
            ?? User.FindFirstValue("upn");

        return Ok(new
        {
            name = User.Identity?.Name,
            objectId = User.FindFirstValue("oid"),
            email,
            roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray(),
        });
    }
}
