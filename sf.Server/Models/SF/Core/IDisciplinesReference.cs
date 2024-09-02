namespace sf.Server.Models.SF.Core;

public interface IDisciplinesReference
{
    public IEnumerable<Discipline>? Disciplines { get; set; }
    
    [NotMapped, JsonProperty("disciplineIds")]
    public IEnumerable<Guid>? DisciplineIds => Disciplines?.Select(d => d.Id);
    
    [NotMapped, JsonProperty("disciplineCount")]
    public long DisciplineCount => Disciplines?.Count() ?? 0;
}