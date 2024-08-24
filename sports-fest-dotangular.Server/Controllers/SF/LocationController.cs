using Microsoft.AspNetCore.Authorization;

namespace sports_fest_dotangular.Server.Controllers.SF;

[Route("api/v1/data/locations"), Authorize]
public class LocationController(SfContext context, DataBaseService<Location> dbService, ResultService resultService)
    : BaseController<Location>(dbService, resultService);