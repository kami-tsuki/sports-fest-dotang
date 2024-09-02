namespace sf.Server.Controllers.SF;

[Route("api/v1/data/location")]
public class LocationController(IServiceProvider services)
    : BaseController<Location>(services);