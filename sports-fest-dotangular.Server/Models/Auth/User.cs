using System.ComponentModel.DataAnnotations.Schema;

namespace sports_fest_dotangular.Server.Models.Auth;

public class User : Entity<Guid>
{
    [MaxLength(256), JsonProperty("first")]
    public string FirstName { get; set; } = Empty;

    [MaxLength(256), JsonProperty("last")]
    public string LastName { get; set; } = Empty;

    [JsonProperty("role")]
    public RoleType Role { get; set; }

    [JsonProperty("classId")]
    public Guid? ClassId { get; set; }
    [ForeignKey(nameof(ClassId)), JsonIgnore]
    public Class? Class { get; init; }

    [InverseProperty(nameof(Entry.Student)), JsonIgnore]
    public IEnumerable<Entry>? Entries { get; init; }

    [NotMapped, JsonProperty("entryIds")]
    public IEnumerable<Guid>? EntryIds => Entries?.Select(e => e.Id);

    [InverseProperty(nameof(Discipline.Users)), JsonIgnore]
    public IEnumerable<Discipline>? Disciplines { get; init; }

    [NotMapped, JsonProperty("disciplineIds")]
    public IEnumerable<Guid>? DisciplineIds => Disciplines?.Select(d => d.Id);
    
    [JsonIgnore, MaxLength(256)]
    public string ApiKey { get; init; } = Empty;
}


public enum RoleType
{
    Student,
    Tutor,
    CampaignManager,
    CampaignJudge
}