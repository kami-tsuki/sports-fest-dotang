using System.Text;
using System.Text.RegularExpressions;
using sf.Server.Models.Auth.Dto;

namespace sf.Server.Services;

public class DataBaseService<TEntity> where TEntity : class, IEntity<Guid>
{
    private readonly SfContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbSet<Entity<Guid>> _auditLogSet;

    public DataBaseService(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<SfContext>();
        _dbSet = _context.Set<TEntity>();
        _auditLogSet = _context.Set<Entity<Guid>>();
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

   public DbSet<TEntity> DbSet => _dbSet;
    public DbSet<Entity<Guid>> AuditLogSet => _auditLogSet;

    public IQueryable<TEntity> GetQueryable() => _dbSet.AsQueryable();

    public async Task<TEntity?> FindAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<int> GetCountAsync(IQueryable<TEntity> query) => await query.CountAsync();

    public async Task<List<TEntity>> GetPagedDataAsync(IQueryable<TEntity> query, long page, long entities)
        => await query.Skip((int)((page - 1) * entities)).Take((int)entities).ToListAsync();

    public void AddEntity(TEntity entity) => _dbSet.Add(entity);

    public void UpdateEntity(TEntity entity) => _context.Entry(entity).State = EntityState.Modified;

    public void RemoveEntity(TEntity entity) => _dbSet.Remove(entity);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, Dictionary<string, string>? filters)
    {
        if (filters == null) return query;

        foreach (var (key, value) in filters)
        {
            var parts = key.Split('-');
            if (parts.Length < 2) continue;

            var propertyName = parts[0];
            var operation = parts[1];

            var property = typeof(TEntity).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase
              | BindingFlags.Public
              | BindingFlags.Instance);
            if (property == null) continue;

            query = operation switch
            {
                "eq" => query.Where(e => EF.Property<object>(e, property.Name).ToString() == value),
                "ne" => query.Where(e => EF.Property<object>(e, property.Name).ToString() != value),
                "gt" => query.Where(e => Compare(EF.Property<object>(e, property.Name).ToString(), value) > 0),
                "ge" => query.Where(e => Compare(EF.Property<object>(e, property.Name).ToString(), value) >= 0),
                "lt" => query.Where(e => Compare(EF.Property<object>(e, property.Name).ToString(), value) < 0),
                "le" => query.Where(e => Compare(EF.Property<object>(e, property.Name).ToString(), value) <= 0),
                "il" => query.Where(
                    e => Regex.IsMatch(
                        EF.Property<object>(e, property.Name).ToString()
                     ?? Empty,
                        WildcardToRegex(value))),
                "nl" => query.Where(
                    e => !Regex.IsMatch(
                        EF.Property<object>(e, property.Name).ToString()
                     ?? Empty,
                        WildcardToRegex(value))),
                "in" => query.Where(e => EF.Property<object>(e, property.Name) == null),
                "nn" => query.Where(e => EF.Property<object>(e, property.Name) != null),
                _    => query
            };
        }

        return query;
    }

    public bool EntityExists(Guid id) => _dbSet.Find(id) != null;

    public Guid GetEntityId(TEntity entity)
    {
        var property = typeof(TEntity).GetProperty("Id", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        return (Guid)(property?.GetValue(entity) ?? Guid.Empty);
    }

    private static string WildcardToRegex(string value) => "^"
                                                         + Regex.Escape(value)
                                                                .Replace("\\*", ".*")
                                                                .Replace("\\%", ".*")
                                                         + "$";

    public TEntity FilterProperties(TEntity entity, string[] properties)
    {
        var clone = Activator.CreateInstance<TEntity>();
        foreach (var property in properties)
        {
            var propInfo = typeof(TEntity).GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propInfo == null) continue;
            var value = propInfo.GetValue(entity);
            propInfo.SetValue(clone, value);
        }
        return clone;
    }

    public void IgnoreNullProperties(TEntity entity)
    {
        foreach (var property in typeof(TEntity).GetProperties())
            if (property.GetValue(entity) == null && property.CanWrite)
                property.SetValue(entity, null);
    }

    public void Detach<TEntity>(TEntity entity) where TEntity : class
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Detached;
    }

    public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(Guid entityId)
    {
        var entity = await _dbSet.FindAsync(entityId);
        var log = await _auditLogSet
                       .Where(e => e.Id == entityId)
                       .Select(e => new AuditLog
                        {
                            Id = e.Id,
                            EntityId = e.Id,
                            EntityType = entity.GetType().Name,
                            Action = "not yet implemented",
                            ChangedBy = "not yet implemented",
                            Timestamp = e.UpdatedAt,
                            Changes = "not yet implemented"
                        })
                       .ToListAsync();
        return log;
    }
        
    public Task<List<TEntity>> GetAllAsync(IQueryable<TEntity> query) => query.ToListAsync();
    
    public string ConvertToCsv(IEnumerable<TEntity> data)
    {
        var properties = typeof(TEntity).GetProperties();
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine(Join(",", properties.Select(p => p.Name)));
        foreach (var item in data)
        {
            var row = properties.Select(p => p.GetValue(item)?.ToString()?.Replace(",", ";"));
            csvBuilder.AppendLine(string.Join(",", row));
        }
        return csvBuilder.ToString();
    }

    
}