namespace sf.Server.Controllers.SF;

[Route("api/v1/data/class"), ApiController, ApiVersion("1.0")]
public class ClassController(IServiceProvider serviceProvider)
    : BaseController<Class>(serviceProvider)
{
    [HttpGet("{id:guid}/school")]
    public async Task<ActionResult<ResultModel<School>>> GetSchool(Guid id)
    {
        Logger.Information("Getting school for class with id {Id}", id);

        var @class = await DbService.FindAsync(id);
        if (@class == null)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Class with id {id} not found"));
        if (!@class.SchoolId.HasValue)
            return NotFound(ResultService.BuildErrorResult("School not found", $"Class with id {id} has no school"));
        return await SchoolController.GetAsync(@class.SchoolId.Value);
    }

    [HttpGet("school/{schoolId:guid}")]
    public Task<ActionResult<ResultModel<Page<Class>>>> GetBySchool(
        Guid schoolId,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        Logger.Information("Getting classes for school with id {SchoolId}", schoolId);
        return BulkGetAsync(page, entities, properties, sendNull, filters.AddFilter(nameof(Class.SchoolId), schoolId));
    }

    [HttpGet("{id:guid}/tutor")]
    public async Task<ActionResult<ResultModel<Tutor>>> GetTutor(Guid id)
    {
        Logger.Information("Getting tutor for class with id {Id}", id);

        var @class = await DbService.FindAsync(id);
        if (@class == null)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Class with id {id} not found"));
        if (!@class.TutorId.HasValue)
            return NotFound(ResultService.BuildErrorResult("Tutor not found", $"Class with id {id} has no tutor"));
        return await TutorController.GetAsync(@class.TutorId.Value);
    }

    [HttpGet("tutor/{tutorId:guid}")]
    public Task<ActionResult<ResultModel<Class>>> GetByTutor(Guid tutorId)
    {
        Logger.Information("Getting classes for tutor with id {TutorId}", tutorId);
        return TutorController.GetClass(tutorId);
    }

    [HttpGet("{id:guid}/students")]
    public Task<ActionResult<ResultModel<Page<Student>>>> GetStudents(
        Guid id,
        long page,
        long entities,
        string? properties,
        bool sendNull,
        Dictionary<string, string>? filters
    )
    {
        Logger.Information("Getting students for class with id {Id}", id);
        return StudentController.GetByClass(id, page, entities, properties, sendNull, filters);
    }
}