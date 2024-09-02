using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Routing;
using sf.Server.Models.Auth.Dto;
using ILogger = Serilog.ILogger;

namespace sf.Server.Controllers.Core;

[ApiController, Route("api/v1/_/base"), ApiVersion("1.0")]
public abstract class BaseController<TEntity>(IServiceProvider services) : ControllerBase
    where TEntity : class, IEntity<Guid>
{
    #region MyRegion

    internal ILogger Logger => services.GetRequiredService<ILogger>();
    internal ResultService ResultService => services.GetRequiredService<ResultService>();
    internal DataBaseService<TEntity> DbService => services.GetRequiredService<DataBaseService<TEntity>>();

    internal DataBaseService<School> SchoolDb => services.GetRequiredService<DataBaseService<School>>();
    internal DataBaseService<Class> ClassDb => services.GetRequiredService<DataBaseService<Class>>();
    internal DataBaseService<Student> StudentDb => services.GetRequiredService<DataBaseService<Student>>();
    internal DataBaseService<Tutor> TutorDb => services.GetRequiredService<DataBaseService<Tutor>>();
    internal DataBaseService<CampaignManager> ManagerDb => services.GetRequiredService<DataBaseService<CampaignManager>>();
    internal DataBaseService<CampaignJudge> JudgeDb => services.GetRequiredService<DataBaseService<CampaignJudge>>();
    internal DataBaseService<Discipline> DisciplineDb => services.GetRequiredService<DataBaseService<Discipline>>();
    internal DataBaseService<Location> LocationDb => services.GetRequiredService<DataBaseService<Location>>();
    internal DataBaseService<Entry> EntryDb => services.GetRequiredService<DataBaseService<Entry>>();
    internal DataBaseService<User> UserDb => services.GetRequiredService<DataBaseService<User>>();

    internal SchoolController SchoolController => services.GetRequiredService<SchoolController>();
    internal ClassController ClassController => services.GetRequiredService<ClassController>();
    internal StudentController StudentController => services.GetRequiredService<StudentController>();
    internal TutorController TutorController => services.GetRequiredService<TutorController>();
    internal ManagerController ManagerController => services.GetRequiredService<ManagerController>();
    internal JudgeController JudgeController => services.GetRequiredService<JudgeController>();
    internal DisciplineController DisciplineController => services.GetRequiredService<DisciplineController>();
    internal LocationController LocationController => services.GetRequiredService<LocationController>();
    internal EntryController EntryController => services.GetRequiredService<EntryController>();
    internal UserController UserController => services.GetRequiredService<UserController>();

    #endregion Services

    #region Bulk

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<ResultModel<Page<TEntity>>>> BulkGetAsync(
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        Logger.Information("Getting all entities of type {EntityType}", typeof(TEntity).Name);

        try
        {
            var query = DbService.ApplyFilters(DbService.GetQueryable(), filters);
            var totalEntities = await DbService.GetCountAsync(query);
            var data = await DbService.GetPagedDataAsync(query, page, entities);

            if (!IsNullOrEmpty(properties)) data = data.Select(entity => DbService.FilterProperties(entity, properties.Split(','))).ToList();

            var pageModel = new Page<TEntity>
            {
                Number = page,
                TotalEntities = totalEntities,
                Data = data
            };
            return Ok(ResultService.BuildResult(true, pageModel));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPost("bulk")]
    public async Task<ActionResult<ResultModel<IEnumerable<TEntity>>>> BulkPostAsync([FromBody] IEnumerable<TEntity> entities)
    {
        Logger.Information("Creating entities of type {EntityType}", typeof(TEntity).Name);

        try
        {
            foreach (var entity in entities)
                DbService.AddEntity(entity);
            await DbService.SaveChangesAsync();
            return Ok(ResultService.BuildResult(true, entities));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPut("bulk")]
    public async Task<ActionResult<ResultModel<IEnumerable<TEntity>>>> BulkPutAsync([FromBody] IEnumerable<TEntity> entities)
    {
        Logger.Information("Updating entities of type {EntityType}", typeof(TEntity).Name);

        try
        {
            foreach (var entity in entities)
            {
                var existingEntity = await DbService.FindAsync(DbService.GetEntityId(entity));
                if (existingEntity == null)
                    return NotFound(ResultService.BuildErrorResult("Entity not found", $"The entity with ID {entity.Id} does not exist."));
                DbService.UpdateEntity(entity);
            }
            await DbService.SaveChangesAsync();
            return Ok(ResultService.BuildResult(true, entities));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpDelete("bulk")]
    public async Task<ActionResult<ResultModel<IEnumerable<Guid>>>> BulkDeleteAsync([FromBody] IEnumerable<Guid> ids)
    {
        Logger.Information("Deleting entities of type {EntityType}", typeof(TEntity).Name);

        try
        {
            foreach (var id in ids)
            {
                var entity = await DbService.FindAsync(id);
                if (entity == null)
                    return NotFound(
                        ResultService.BuildErrorResult(
                            "Entity not found",
                            $"The entity with ID {id} does not exist."));
                DbService.RemoveEntity(entity);
            }
            await DbService.SaveChangesAsync();
            return Ok(ResultService.BuildResult(true, ids));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<ResultModel<Page<TEntity>>>> SearchAsync(
        [FromQuery] string query,
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        Logger.Information("Searching for entities of type {EntityType} with query {Query}", typeof(TEntity).Name, query);

        try
        {
            var queryable = DbService.ApplyFilters(DbService.GetQueryable(), filters);
            if (!IsNullOrEmpty(query))
            {
                var parameter = Expression.Parameter(typeof(TEntity), "entity");
                var props = typeof(TEntity).GetProperties();
                var predicate = (
                    from property in props
                    where property.PropertyType == typeof(string)
                    select Expression.Call(
                        Expression.Call(
                            Expression.MakeMemberAccess(parameter, property),
                            typeof(string).GetMethod("ToLower", Type.EmptyTypes)
                        ),
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                        Expression.Constant(query.ToLower())
                    )).Aggregate<MethodCallExpression?, Expression?>(null, (current, containsExpression) => current == null ? containsExpression : Expression.OrElse(current, containsExpression));

                if (predicate != null)
                {
                    var lambda = Expression.Lambda<Func<TEntity, bool>>(predicate, parameter);
                    queryable = queryable.Where(lambda);
                }
            }

            var totalEntities = await DbService.GetCountAsync(queryable);
            var data = await DbService.GetPagedDataAsync(queryable, page, entities);
            if (!IsNullOrEmpty(properties))
                data = data.Select(entity => DbService.FilterProperties(entity, properties.Split(','))).ToList();
            var pageModel = new Page<TEntity>
            {
                Number = page,
                TotalEntities = totalEntities,
                Data = data
            };
            return Ok(ResultService.BuildResult(true, pageModel));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult<ResultModel<long>>> GetCountAsync([FromQuery] Dictionary<string, string>? filters = null)
    {
        Logger.Information("Getting count of entities of type {EntityType}", typeof(TEntity).Name);

        try
        {
            var query = DbService.ApplyFilters(DbService.GetQueryable(), filters);
            var count = await DbService.GetCountAsync(query);

            return Ok(ResultService.BuildResult(true, count));
        }
        catch (Exception ex)
        {
            return BadRequest(
                ResultService
                   .BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    #endregion

    #region Id

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> GetAsync(Guid id, [FromQuery] string? properties = null)
    {
        Logger.Information("Getting entity of type {EntityType} with ID {EntityId}", typeof(TEntity).Name, id);

        try
        {
            var entity = await DbService.FindAsync(id);
            if (entity == null)
                return NotFound(ResultService.BuildErrorResult("Entity not found", "The requested entity does not exist."));

            if (!IsNullOrEmpty(properties)) entity = DbService.FilterProperties(entity, properties.Split(','));

            return Ok(ResultService.BuildResult(true, entity));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResultModel<TEntity>>> PostAsync(TEntity entity, [FromQuery] bool ignoreNullProperties = false)
    {
        Logger.Information("Creating entity of type {EntityType}", typeof(TEntity).Name);

        try
        {
            if (ignoreNullProperties) DbService.IgnoreNullProperties(entity);

            var existingEntity = await DbService.FindAsync(DbService.GetEntityId(entity));
            if (existingEntity != null)
                return Conflict(ResultService.BuildErrorResult("Entity conflict", "An entity with the same key already exists."));

            DbService.AddEntity(entity);
            await DbService.SaveChangesAsync();

            var result = ResultService.BuildResult(true, entity);

            return CreatedAtAction(nameof(GetAsync), new { id = DbService.GetEntityId(entity) }, result);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> PutAsync(Guid id, TEntity entity, [FromQuery] bool ignoreNullProperties = false)
    {
        Logger.Information("Updating entity of type {EntityType} with ID {EntityId}", typeof(TEntity).Name, id);

        try
        {
            var existingEntity = await DbService.FindAsync(id);
            if (existingEntity == null)
                return NotFound(ResultService.BuildErrorResult("Entity not found", "The entity to update does not exist."));

            if (ignoreNullProperties) DbService.IgnoreNullProperties(entity);

            DbService.Detach(existingEntity);
            entity.Id = id;
            DbService.UpdateEntity(entity);
            await DbService.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPut]
    public async Task<ActionResult<ResultModel<TEntity>>> PutAsync(TEntity entity, [FromQuery] bool ignoreNullProperties = false)
    {
        Logger.Information("Updating entity of type {EntityType}", typeof(TEntity).Name);

        try
        {
            var existingId = DbService.GetEntityId(entity);
            if (existingId == Guid.Empty)
            {
                if (ignoreNullProperties) DbService.IgnoreNullProperties(entity);
                DbService.AddEntity(entity);
                await DbService.SaveChangesAsync();

                var result = ResultService.BuildResult(true, entity);
                return CreatedAtAction(nameof(GetAsync), new { id = DbService.GetEntityId(entity) }, result);
            }

            var existingEntity = await DbService.FindAsync(existingId);
            if (existingEntity == null)
            {
                if (ignoreNullProperties) DbService.IgnoreNullProperties(entity);
                DbService.AddEntity(entity);
                await DbService.SaveChangesAsync();

                var result = ResultService.BuildResult(true, entity);
                return CreatedAtAction(nameof(GetAsync), new { id = DbService.GetEntityId(entity) }, result);
            }

            if (ignoreNullProperties) DbService.IgnoreNullProperties(existingEntity);
            DbService.UpdateEntity(existingEntity);
            await DbService.SaveChangesAsync();
            return Ok(ResultService.BuildResult(true, entity));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }


    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> PatchAsync(Guid id, [FromBody] JsonPatchDocument<TEntity>? patchDoc)
    {
        Logger.Information("Patching entity of type {EntityType} with ID {EntityId}", typeof(TEntity).Name, id);

        if (patchDoc == null)
            return BadRequest(ResultService.BuildErrorResult("Invalid patch document", "The patch document is null."));

        try
        {
            var entity = await DbService.FindAsync(id);
            if (entity == null)
                return NotFound(ResultService.BuildErrorResult("Entity not found", "The entity to update does not exist."));

            patchDoc.ApplyTo(entity);

            if (!ModelState.IsValid)
                return BadRequest(ResultService.BuildErrorResult("Invalid model state", "The patch document is invalid."));

            DbService.UpdateEntity(entity);
            await DbService.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(ResultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> DeleteAsync(Guid id)
    {
        Logger.Information("Deleting entity of type {EntityType} with ID {EntityId}", typeof(TEntity).Name, id);

        try
        {
            var entity = await DbService.FindAsync(id);
            if (entity == null) return NotFound(ResultService.BuildErrorResult("Entity not found", "The entity to delete does not exist."));
            DbService.RemoveEntity(entity);
            await DbService.SaveChangesAsync();
            return Ok(ResultService.BuildResult(true, entity));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    #endregion

    #region Additonal

    [HttpOptions]
    public ActionResult<ResultModel<object>> Options(
        [FromQuery] string? path = null,
        [FromQuery] bool includeModels = false,
        [FromQuery] bool includeHttpCodes = false)
    {
        Logger.Information("Getting options for controller {ControllerType}", GetType().Name);

        var controllerType = GetType();
        var actions = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        var methods = (from method in actions
            let httpAttributes = method.GetCustomAttributes(typeof(HttpMethodAttribute), false)
            from HttpMethodAttribute attr in httpAttributes
            where path == null
               || method.GetCustomAttributes(typeof(RouteAttribute), false)
                        .Any(r => ((RouteAttribute)r).Template?.Contains(path) == true)
            select attr.HttpMethods.First()).ToList();

        var response = new
        {
            AllowedMethods = methods.Distinct().ToArray(),
            Models = includeModels ? GetModels(actions) : null,
            HttpCodes = includeHttpCodes ? GetHttpCodes(actions) : null
        };
        Response.Headers.Append("Allow", Join(", ", methods.Distinct()));
        return Ok(ResultService.BuildResult(true, response));
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportAsync([FromQuery] string format = "csv", [FromQuery] Dictionary<string, string>? filters = null)
    {
        Logger.Information("Exporting entities of type {EntityType}", typeof(TEntity).Name);

        try
        {
            var query = DbService.ApplyFilters(DbService.GetQueryable(), filters);
            var data = await DbService.GetAllAsync(query);

            switch (format.ToLower())
            {
                case "csv":
                {
                    var csvData = DbService.ConvertToCsv(data);
                    return File(new UTF8Encoding().GetBytes(csvData), "text/csv", "export.csv");
                }
                case "excel":
                    return BadRequest(ResultService.BuildErrorResult("Excel export not supported", "Excel export is not yet supported."));
                case "json":
                    return Ok(ResultService.BuildResult(true, data));
                case "xml":
                    return BadRequest(ResultService.BuildErrorResult("Excel export not supported", "Excel export is not yet supported."));
                default: return BadRequest(ResultService.BuildErrorResult("Invalid format", "Supported formats are 'csv' and 'excel'."));
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPost("validate")]
    public ActionResult<ResultModel<object>> ValidateAsync([FromBody] TEntity entity)
    {
        Logger.Information("Validating entity of type {EntityType}", typeof(TEntity).Name);

        try
        {
            if (entity == null)
                return BadRequest(ResultService.BuildErrorResult("Validation failed", "Entity is null"));

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(entity, null, null);

            // ValidateAsync entity using the built-in .NET Validator
            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorMessages = validationResults.Select(vr => $"{vr.ErrorMessage}").ToList();
                return BadRequest(ResultService.BuildErrorResult("Validation failed", Join(", ", errorMessages)));
            }
            CustomValidation(entity, validationResults);
            if (!validationResults.Any())
                return Ok(ResultService.BuildResult(true, "Entity is valid"));
            var customErrorMessages = validationResults.Select(vr => $"{vr.ErrorMessage}").ToList();
            return BadRequest(ResultService.BuildErrorResult("Validation failed", Join(", ", customErrorMessages)));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while validating the entity", ex.Message));
        }
    }

    [HttpGet("{id:guid}/audit")]
    public async Task<ActionResult<ResultModel<IEnumerable<AuditLog>>>> GetAuditHistoryAsync([FromRoute] Guid id)
    {
        Logger.Information("Getting audit history for entity of type {EntityType} with ID {EntityId}", typeof(TEntity).Name, id);

        try
        {
            var auditLogs = await DbService.GetAuditLogsAsync(id);
            if (auditLogs == null || !auditLogs.Any())
                return NotFound(ResultService.BuildErrorResult("No audit history found", "The entity has no audit history."));
            return Ok(ResultService.BuildResult(true, auditLogs));
        }
        catch (Exception ex)
        {
            return BadRequest(ResultService.BuildErrorResult("An error occurred while retrieving audit history", ex.Message));
        }
    }

    #endregion

    #region methods

    private object GetModels(MethodInfo[] actions)
    {
        Logger.Information("Getting models for controller {ControllerType}", GetType().Name);

        var models = new Dictionary<string, object>();
        foreach (var method in actions)
        {
            var parameters = method.GetParameters();
            var returnType = method.ReturnType;

            models[method.Name] = new
            {
                Input = parameters.Select(p => new { p.Name, Type = p.ParameterType.Name }).ToArray(),
                Output = returnType.Name
            };
        }
        return models;
    }


    [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global"),
     SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    internal virtual void CustomValidation(TEntity entity, List<ValidationResult> validationResults)
    {
        if (ValidateProperty(entity, nameof(Entity<Guid>.Id), out var result))
            validationResults.Add(new(result.Item1, result.Item2));
        if (ValidateProperty(entity, nameof(Entity<Guid>.CreatedAt), out result))
            validationResults.Add(new(result.Item1, result.Item2));
        if (ValidateProperty(entity, nameof(Entity<Guid>.UpdatedAt), out result))
            validationResults.Add(new(result.Item1, result.Item2));
    }

    private bool ValidateProperty(TEntity entity, string property, out (string, string[]) result)
    {
        var prop = typeof(TEntity).GetProperty(property);
        if (prop == null)
        {
            result = ("Invalid property", [property]);
            return false;
        }
        var obj = prop.GetValue(entity);
        if (obj == null)
        {
            result = ("Property is null", [property]);
            return false;
        }
        result = ("Property is valid", [property]);
        return true;
    }


    private object GetHttpCodes(MethodInfo[] actions)
    {
        var httpCodes = new Dictionary<string, int[]>();
        foreach (var method in actions)
        {
            var attributes = method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), false);
            httpCodes[method.Name] = attributes.Select(a => ((ProducesResponseTypeAttribute)a).StatusCode).ToArray();
        }
        return httpCodes;
    }

    #endregion
}