namespace sf.Server.Controllers.SF;

[Route("api/v1/user/student")]
public class StudentController(IServiceProvider services)
    : BaseController<Student>(services)
{
    
    
    [HttpGet("{id:guid}/class")]
    public async Task<ActionResult<ResultModel<Class>>> GetClass(Guid id)
    {
        var student = await DbService.FindAsync(id);
        if (student == null) return NotFound(ResultService.BuildErrorResult("Student not found", $"Student with id {id} not found"));
        if (!student.ClassId.HasValue) return NotFound(ResultService.BuildErrorResult("Class not found", $"Class for student with id {id} not found"));
        return await ClassController.GetAsync(student.ClassId.Value);
    }

    [HttpGet("{id:guid}/school")]
    public Task<ActionResult<ResultModel<School>>> GetSchool(Guid id)
        => SchoolController.GetByStudent(id);

    [HttpGet("class/{classId:guid}")]
    public async Task<ActionResult<ResultModel<Page<Student>>>> GetByClass(
        Guid classId,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        var @class = await ClassDb.FindAsync(classId);
        if (@class == null) return NotFound(ResultService.BuildErrorResult("Class not found", $"Class with id {classId} not found"));
        return await BulkGetAsync(page, entities, properties, sendNull, filters.AddFilter(nameof(Student.ClassId), classId));
    }

    [HttpGet("school/{schoolId:guid}")]
    public async Task<ActionResult<ResultModel<Page<Student>>>> GetBySchool(
        Guid schoolId,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        var school = await SchoolDb.FindAsync(schoolId);
        if (school == null) return NotFound(ResultService.BuildErrorResult("School not found", $"School with id {schoolId} not found"));
        return await BulkGetAsync(page, entities, properties, sendNull, filters.AddFilter(nameof(Student.SchoolId), schoolId));
    }
}