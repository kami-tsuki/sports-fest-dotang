namespace sf.Server.Models.SF;

[Index(nameof(ShortName), IsUnique = true)]
public class Location : Entity<Guid>, IName, IComment, IClassesReference, IDisciplinesReference
{
    [MaxLength(64), MinLength(2), JsonProperty("name")]
    public string Name { get; set; } = Empty;

    [MaxLength(8), MinLength(2), JsonProperty("short")]
    public string ShortName { get; set; } = Empty;

    
    [MaxLength(1024), JsonProperty("comment")]
    public string Comment { get; set; } = Empty;
    

    [InverseProperty(nameof(Class.Room)), JsonIgnore]
    public IEnumerable<Class>? Classes { get; set; }

    [JsonProperty("classIds"), NotMapped]
    public IEnumerable<Guid> ClassIds => Classes?.Select(c => c.Id).ToList() ?? [];


    [InverseProperty(nameof(Discipline.Location)), JsonIgnore]
    public IEnumerable<Discipline>? Disciplines { get; set; }

    [JsonProperty("disciplineIds"), NotMapped]
    public IEnumerable<Guid>? DisciplineIds => Disciplines?.Select(d => d.Id);
}