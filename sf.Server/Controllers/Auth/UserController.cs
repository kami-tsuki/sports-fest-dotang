namespace sf.Server.Controllers.Auth;

[Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
public class UserController(IServiceProvider services)
    : BaseController<User>(services)
{
}