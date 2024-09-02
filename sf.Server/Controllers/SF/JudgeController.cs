namespace sf.Server.Controllers.SF;

[Route("api/v1/user/judge")]
public class JudgeController(IServiceProvider services)
    : BaseController<CampaignJudge>(services)
{

}