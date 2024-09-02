namespace sf.Server.Controllers.SF;

[Route("api/v1/user/manager")]
public class ManagerController(IServiceProvider services)
    : BaseController<CampaignManager>(services);