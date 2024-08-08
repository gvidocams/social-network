using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult Register(UserRegistrationRequest userRegistrationRequest)
    {
        return Ok(userRegistrationRequest);
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetOk()
    {
        return Ok();
    }
}