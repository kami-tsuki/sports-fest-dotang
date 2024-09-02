namespace sf.Server.Models.SF.Core;

public interface IClassesReference
{
    public IEnumerable<Class>? Classes { get; set; }
    [JsonProperty("classIds"), NotMapped]
    public IEnumerable<Guid> ClassIds => Classes?.Select(c => c.Id).ToList() ?? new List<Guid>();
    public long ClassCount => Classes?.Count() ?? 0;
}