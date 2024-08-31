using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialNetwork.Infrastructure;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationRequest userRegistrationRequest)
    {
        var user = new ApplicationUser
        {
            Email = userRegistrationRequest.Email,
            UserName = userRegistrationRequest.Email,
            Name = userRegistrationRequest.Name,
            MiddleName = userRegistrationRequest.MiddleName,
            Surname = userRegistrationRequest.Surname,
            DateOfBirth = userRegistrationRequest.DateOfBirth,
            Interests = userRegistrationRequest.Interests
        };

        var result = await _userManager.CreateAsync(user, userRegistrationRequest.Password);

        if (!result.Succeeded) return BadRequest(JsonConvert.SerializeObject(result));

        await _signInManager.SignInAsync(user, isPersistent: false);
            
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(userLoginRequest.Email, userLoginRequest.Password, false, false);

        if (result.Succeeded)
        {
            return Ok();
        }

        return Unauthorized("Email or password is invalid");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserDateOfBirth()
    {
        var accessor = new HttpContextAccessor();

        var userId = accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return NoContent();
        }

        _applicationDbContext.Posts.Add(new Post { ApplicationUser = user, Subject = "Test" });
        await _applicationDbContext.SaveChangesAsync();

        return Ok(string.Join(' ', user.Interests));
    }
}