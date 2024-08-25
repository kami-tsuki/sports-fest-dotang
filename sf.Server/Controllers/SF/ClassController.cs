using Microsoft.AspNetCore.Authorization;
using sf.Server.Controllers.Core;
using sf.Server.Data.Sf;
using sf.Server.Models.Result;
using sf.Server.Models.SF;
using sf.Server.Services;
using static Microsoft.EntityFrameworkCore.EF;

namespace sf.Server.Controllers.SF;

/// <inheritdoc />
[Route("api/v1/data/classes"), AllowAnonymous]
public class ClassController(SfContext context, DataBaseService<Class> dbService, ResultService resultService)
    : BaseController<Class>(dbService, resultService)
{
    private readonly ResultService _resultService = resultService;
    private readonly DataBaseService<Class> _dbService = dbService;

    
}