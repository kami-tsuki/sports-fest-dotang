namespace sf.Server.Controllers.SF;

[Route("api/v1/user/tutor")]
public class TutorController(IServiceProvider services)
    : BaseController<Tutor>(services)
{
    #region tutor
    
    [HttpGet("{id:guid}/class")]
    public async Task<ActionResult<ResultModel<Class>>> GetClass(Guid id)
    {
        var tutor = await DbService.FindAsync(id);
        if (tutor == null)
            return NotFound(ResultService.BuildErrorResult("Tutor not found", $"Tutor with id {id} not found"));
        if (!tutor.ClassId.HasValue)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Tutor with id {id} has no class"));
        return await ClassController.GetAsync(tutor.ClassId.Value);
    }

    [HttpGet("class/{classId:guid}")]
    public Task<ActionResult<ResultModel<Tutor>>> GetByClass(Guid classId) => ClassController.GetTutor(classId);
    
    #endregion

    #region School
    
    [HttpGet("{id:guid}/school")]
    public async Task<ActionResult<ResultModel<School>>> GetSchool(Guid id)
    {
        var tutor = await DbService.FindAsync(id);
        if (tutor == null)
            return NotFound(ResultService.BuildErrorResult("Tutor not found", $"Tutor with id {id} not found"));
        return await SchoolController.GetAsync(tutor.SchoolId);
    }

    #endregion
    
    #region students
    
    [HttpGet("{id:guid}/students")]
    
    public async Task<ActionResult<ResultModel<Page<Student>>>> GetStudents(
        Guid id,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        var tutor = await TutorDb.FindAsync(id);
        if (tutor == null)
            return NotFound(ResultService.BuildErrorResult("Tutor not found", $"Tutor with id {id} not found"));
        if (!tutor.ClassId.HasValue)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Tutor with id {id} has no class"));
        var @class = await ClassDb.FindAsync(tutor.ClassId.Value);
        if (@class == null)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Class with id {tutor.ClassId} not found"));
        return await StudentController.GetByClass(@class.Id, page, entities, properties, sendNull, filters);
    }
    
    #endregion
    
}