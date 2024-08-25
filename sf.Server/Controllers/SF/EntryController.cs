using Microsoft.AspNetCore.Authorization;
using sf.Server.Controllers.Core;
using sf.Server.Data.Sf;
using sf.Server.Models.SF;
using sf.Server.Services;

namespace sf.Server.Controllers.SF;

[Route("api/v1/data/entries"), Authorize]
public class EntryController(SfContext context, DataBaseService<Entry> dbService, ResultService resultService)
    : BaseController<Entry>(dbService, resultService);