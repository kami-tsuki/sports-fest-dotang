using System.ComponentModel.DataAnnotations.Schema;

namespace sports_fest_dotangular.Server.Models.SF;

[Index(nameof(ShortName), IsUnique = true)]
public class Location : Entity<Guid>
{
    [MaxLength(64), JsonProperty("name")]
    public string Name { get; set; } = Empty;

    [MaxLength(8), JsonProperty("short")]
    public string ShortName { get; set; } = Empty;

    [MaxLength(1024), JsonProperty("description")]
    public string Comment { get; set; } = Empty;

    [InverseProperty(nameof(Class.Room)), JsonIgnore]
    public IEnumerable<Class>? Classes { get; init; }

    [JsonProperty("classIds"), NotMapped]
    public IEnumerable<Guid>? ClassIds => Classes?.Select(c => c.Id);


    [InverseProperty(nameof(Discipline.Location)), JsonIgnore]
    public IEnumerable<Discipline>? Disciplines { get; init; }

    [JsonProperty("disciplineIds"), NotMapped]
    public IEnumerable<Guid>? DisciplineIds => Disciplines?.Select(d => d.Id);
}