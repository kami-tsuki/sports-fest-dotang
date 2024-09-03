namespace sf.Server.Controllers.Auth;

[Route("api/v1/user")]
public class UserController(IServiceProvider services)
    : BaseController<User>(services)
{
}