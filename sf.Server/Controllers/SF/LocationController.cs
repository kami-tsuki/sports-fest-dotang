using Microsoft.AspNetCore.Authorization;
using sf.Server.Controllers.Core;
using sf.Server.Data.Sf;
using sf.Server.Models.SF;
using sf.Server.Services;

namespace sf.Server.Controllers.SF;

[Route("api/v1/data/locations"), Authorize]
public class LocationController(SfContext context, DataBaseService<Location> dbService, ResultService resultService)
    : BaseController<Location>(dbService, resultService);