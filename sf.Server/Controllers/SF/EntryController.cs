namespace sf.Server.Controllers.SF;

[Route("api/v1/data/entry")]
public class EntryController(IServiceProvider services)
    : BaseController<Entry>(services);