using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.Routing;

namespace sports_fest_dotangular.Server.Controllers.Core;

[ApiController, Route("api/v1/_/[controller]"), ApiVersion("1.0"), AllowAnonymous]
public abstract class BaseController<TEntity>(DataBaseService<TEntity> dataBaseService, ResultService resultService) : ControllerBase
    where TEntity : Entity<Guid>
{
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<ResultModel<Page<TEntity>>>> GetAll(
        [FromQuery] long page = 1,
        [FromQuery] long entities = 10,
        [FromQuery] string? properties = null,
        [FromQuery] bool sendNull = false,
        [FromQuery] Dictionary<string, string>? filters = null)
    {
        try
        {
            var query = dataBaseService.ApplyFilters(dataBaseService.GetQueryable(), filters);
            var totalEntities = await dataBaseService.GetCountAsync(query);
            var data = await dataBaseService.GetPagedDataAsync(query, page, entities);

            if (!IsNullOrEmpty(properties)) data = data.Select(entity => dataBaseService.FilterProperties(entity, properties.Split(','))).ToList();

            var pageModel = new Page<TEntity>
            {
                Number = page,
                TotalEntities = totalEntities,
                Data = data
            };
            return Ok(resultService.BuildResult(true, pageModel));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> Get(Guid id, [FromQuery] string? properties = null)
    {
        try
        {
            var entity = await dataBaseService.FindAsync(id);
            if (entity == null) return NotFound(resultService.BuildErrorResult("Entity not found", "The requested entity does not exist."));

            if (!IsNullOrEmpty(properties)) entity = dataBaseService.FilterProperties(entity, properties.Split(','));

            return Ok(resultService.BuildResult(true, entity));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResultModel<TEntity>>> Post(TEntity entity, [FromQuery] bool ignoreNullProperties = false)
    {
        try
        {
            if (ignoreNullProperties) dataBaseService.IgnoreNullProperties(entity);

            var existingEntity = await dataBaseService.FindAsync(dataBaseService.GetEntityId(entity));
            if (existingEntity != null)
                return Conflict(resultService.BuildErrorResult("Entity conflict", "An entity with the same key already exists."));

            dataBaseService.AddEntity(entity);
            await dataBaseService.SaveChangesAsync();

            var result = resultService.BuildResult(true, entity);

            return CreatedAtAction(nameof(Get), new { id = dataBaseService.GetEntityId(entity) }, result);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(resultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPut("{id:guid}"), ]
    public async Task<ActionResult<ResultModel<TEntity>>> Put(Guid id, TEntity entity, [FromQuery] bool ignoreNullProperties = false)
    {
        try
        {
            if (!dataBaseService.EntityExists(id))
                return NotFound(resultService.BuildErrorResult("Entity not found", "The entity to update does not exist."));

            if (ignoreNullProperties) dataBaseService.IgnoreNullProperties(entity);

            dataBaseService.UpdateEntity(entity);
            await dataBaseService.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(resultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpPut]
    public async Task<ActionResult<ResultModel<TEntity>>> Put(TEntity entity, [FromQuery] bool ignoreNullProperties = false)
    {
        try
        {
            var existingId = dataBaseService.GetEntityId(entity);
            if (existingId == Guid.Empty)
            {
                if (ignoreNullProperties) dataBaseService.IgnoreNullProperties(entity);
                dataBaseService.AddEntity(entity);
                await dataBaseService.SaveChangesAsync();

                var result = resultService.BuildResult(true, entity);
                return CreatedAtAction(nameof(Get), new { id = dataBaseService.GetEntityId(entity) }, result);
            }

            var existingEntity = await dataBaseService.FindAsync(existingId);
            if (existingEntity == null)
            {
                if (ignoreNullProperties) dataBaseService.IgnoreNullProperties(entity);
                dataBaseService.AddEntity(entity);
                await dataBaseService.SaveChangesAsync();

                var result = resultService.BuildResult(true, entity);
                return CreatedAtAction(nameof(Get), new { id = dataBaseService.GetEntityId(entity) }, result);
            }

            if (ignoreNullProperties) dataBaseService.IgnoreNullProperties(existingEntity);
            dataBaseService.UpdateEntity(existingEntity);
            await dataBaseService.SaveChangesAsync();
            return Ok(resultService.BuildResult(true, entity));
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(resultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }
        
        

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> Patch(Guid id, [FromBody] JsonPatchDocument<TEntity>? patchDoc)
    {
        if (patchDoc == null)
            return BadRequest(resultService.BuildErrorResult("Invalid patch document", "The patch document is null."));

        try
        {
            var entity = await dataBaseService.FindAsync(id);
            if (entity == null)
                return NotFound(resultService.BuildErrorResult("Entity not found", "The entity to update does not exist."));

            patchDoc.ApplyTo(entity);

            if (!ModelState.IsValid)
                return BadRequest(resultService.BuildErrorResult("Invalid model state", "The patch document is invalid."));

            dataBaseService.UpdateEntity(entity);
            await dataBaseService.SaveChangesAsync();

            return NoContent();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(resultService.BuildErrorResult("Database update error", ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ResultModel<TEntity>>> Delete(Guid id)
    {
        try
        {
            var entity = await dataBaseService.FindAsync(id);
            if (entity == null) return NotFound(resultService.BuildErrorResult("Entity not found", "The entity to delete does not exist."));
            dataBaseService.RemoveEntity(entity);
            await dataBaseService.SaveChangesAsync();
            return Ok(resultService.BuildResult(true, entity));
        }
        catch (Exception ex)
        {
            return BadRequest(resultService.BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult<ResultModel<long>>> GetCount([FromQuery] Dictionary<string, string>? filters = null)
    {
        try
        {
            var query = dataBaseService.ApplyFilters(dataBaseService.GetQueryable(), filters);
            var count = await dataBaseService.GetCountAsync(query);

            return Ok(resultService.BuildResult(true, count));
        }
        catch (Exception ex)
        {
            return BadRequest(
                resultService
                   .BuildErrorResult("An error occurred while processing the request", ex.Message));
        }
    }

    [HttpOptions]
    public ActionResult<ResultModel<object>> Options(
        [FromQuery] string? path = null,
        [FromQuery] bool includeModels = false,
        [FromQuery] bool includeHttpCodes = false)
    {
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
        return Ok(resultService.BuildResult(true, response));
    }

    private object GetModels(MethodInfo[] actions)
    {
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

    //Set headers method
    protected void SetHeaders(HttpResponse response, string key, string value) => response.Headers.Append(key, value);

        
}