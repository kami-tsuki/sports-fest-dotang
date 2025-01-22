using sf.Server.Controllers.Core;
using sf.Server.Models.SF;

namespace sf.Server.Controllers;

[Route("api/[controller]"), ApiController]
public partial class ClassController(IServiceProvider services)
    : BaseController<ClassModel>(services);

[Route("api/[controller]"), ApiController]
public partial class UserController(IServiceProvider services)
    : BaseController<UserModel>(services);

[Route("api/[controller]"), ApiController]
public partial class DisciplineController(IServiceProvider services)
    : BaseController<ClassModel>(services);

[Route("api/[controller]"), ApiController]
public partial class TeamController(IServiceProvider services)
    : BaseController<TeamModel>(services);