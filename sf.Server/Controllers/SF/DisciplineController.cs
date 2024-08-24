using Microsoft.AspNetCore.Authorization;

namespace sports_fest_dotangular.Server.Controllers.SF;

[Route("api/v1/data/disciplines"), Authorize]
public class DisciplineController(SfContext context, DataBaseService<Discipline> dbService, ResultService resultService)
    : BaseController<Discipline>(dbService, resultService);