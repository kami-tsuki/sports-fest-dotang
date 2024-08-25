using Microsoft.AspNetCore.Authorization;
using sf.Server.Controllers.Core;
using sf.Server.Data.Sf;
using sf.Server.Models.SF;
using sf.Server.Services;

namespace sf.Server.Controllers.SF;

[Route("api/v1/data/disciplines"), Authorize]
public class DisciplineController(SfContext context, DataBaseService<Discipline> dbService, ResultService resultService)
    : BaseController<Discipline>(dbService, resultService);