using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace sports_fest_dotangular.Server.Controllers.Auth;

[Microsoft.AspNetCore.Components.Route("api/v1/data/users")]
public class UserController(DbContext context, DataBaseService<User> dbService, ResultService resultService)
    : BaseController<User>(dbService, resultService)
{
}