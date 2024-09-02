namespace sf.Server.Models.SF.Core;

public interface IStudentsReference
{
    
    [JsonProperty("studentIds"), NotMapped]
    public IEnumerable<Guid> StudentIds => Students?.Select(s => s.Id).ToList() ?? new List<Guid>();
    public IEnumerable<Student>? Students { get; set; }
    
    
    [NotMapped, JsonProperty("studentCount")]
    public long StudentCount => Students?.LongCount() ?? 0;
}
