using Microsoft.AspNetCore.Authorization;

namespace sports_fest_dotangular.Server.Controllers.SF;

[Route("api/v1/data/entries"), Authorize]
public class EntryController(SfContext context, DataBaseService<Entry> dbService, ResultService resultService)
    : BaseController<Entry>(dbService, resultService);