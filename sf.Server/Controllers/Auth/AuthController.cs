// File: sf.Server/Controllers/AuthController.cs

using Microsoft.AspNetCore.Identity;
using sf.Server.Models.Auth.Dto;

namespace sf.Server.Controllers.Auth;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var user = new CampaignManager()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Email,
            Role = RoleType.CampaignManager,
        };
        if (model.Password is null) return BadRequest("Password is required");

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            // Assign user as manager of the school
            // Create school and assign user as manager
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
        if (result.Succeeded)
        {
            return Ok();
        }
        return Unauthorized();
    }
}