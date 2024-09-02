namespace sf.Server.Models.Auth.Dto;

public class AuditLog
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; }
    public string? EntityType { get; set; }
    public string Action { get; set; }
    public string ChangedBy { get; set; } 
    public DateTime Timestamp { get; set; } 
    public string Changes { get; set; } 
}
