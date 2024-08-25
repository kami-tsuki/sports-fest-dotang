using sf.Server.Controllers.Core;
using sf.Server.Models.Auth;
using sf.Server.Services;

namespace sf.Server.Controllers.Auth;

[Microsoft.AspNetCore.Components.Route("api/v1/auth/users")]
public class UserController(DbContext context, DataBaseService<User> dbService, ResultService resultService)
    : BaseController<User>(dbService, resultService)
{
}