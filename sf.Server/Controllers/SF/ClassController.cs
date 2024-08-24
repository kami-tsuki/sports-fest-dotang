using Microsoft.AspNetCore.Authorization;

namespace sports_fest_dotangular.Server.Controllers.SF;

/// <inheritdoc />
[Route("api/v1/data/classes"), AllowAnonymous]
public class ClassController(SfContext context, DataBaseService<Class> dbService, ResultService resultService)
    : BaseController<Class>(dbService, resultService)
{
}