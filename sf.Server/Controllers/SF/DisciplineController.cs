namespace sf.Server.Controllers.SF;

[Route("api/v1/data/[controller]")]
public class DisciplineController(IServiceProvider services)
    : BaseController<Discipline>(services);