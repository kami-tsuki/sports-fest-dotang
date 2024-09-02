namespace sf.Server.Controllers.SF;

[Route("api/v1/data/school")]
public class SchoolController(IServiceProvider serviceProvider)
    : BaseController<School>(serviceProvider)
{
    #region Class

    [HttpGet("{id:guid}/classes")]
    public async Task<ActionResult<ResultModel<Page<Class>>>> GetClasses(
        Guid id,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
        => await ClassController.GetBySchool(id, page, entities, properties, sendNull, filters);

    [HttpGet("class/{classId:guid}")]
    public async Task<ActionResult<ResultModel<School>>> GetByClass(Guid classId)
    {
        var @class = await ClassDb.FindAsync(classId);
        if (@class == null)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Class with id {classId} not found"));
        if (!@class.SchoolId.HasValue)
            return NotFound(ResultService.BuildErrorResult("School not found", $"Class with id {classId} has no school"));
        return await GetAsync(@class.SchoolId.Value);
    }


    [HttpPost("{id:guid}/class")]
    public async Task<ActionResult<ResultModel<Class>>> CreateClass(Guid id, [FromBody] Class @class)
    {
        var school = await DbService.FindAsync(id);
        if (school == null)
            return NotFound(ResultService.BuildErrorResult("School not found", $"School with id {id} not found"));
        @class.SchoolId = id;
        return await ClassController.PostAsync(@class);
    }

    #endregion

    #region students

    [HttpGet("{id:guid}/students")]
    public Task<ActionResult<ResultModel<Page<Student>>>> GetStudents(
        Guid id,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
        => StudentController.GetBySchool(id, page, entities, properties, sendNull, filters);

    [HttpGet("student/{studentId:guid}")]
    public async Task<ActionResult<ResultModel<School>>> GetByStudent(Guid studentId)
    {
        var student = await StudentDb.FindAsync(studentId);
        if (student == null)
            return NotFound(ResultService.BuildErrorResult("Student not found", $"Student with id {studentId} not found"));
        if (!student.ClassId.HasValue)
            return NotFound(ResultService.BuildErrorResult("Class not found", $"Student with id {studentId} has no class"));
        return await GetAsync(student.SchoolId);
    }

    #endregion

    #region Tutor

    [HttpGet("{id:guid}/tutors")]
    public Task<ActionResult<ResultModel<Page<Tutor>>>> GetTutorsAsync(
        Guid id,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
        => TutorController.BulkGetAsync(page, entities, properties, sendNull, filters.AddFilter(nameof(Tutor.SchoolId), id));

    #endregion
}