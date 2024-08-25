using System.ComponentModel.DataAnnotations.Schema;
using sf.Server.Models.Auth;
using sf.Server.Models.Core;

namespace sf.Server.Models.SF;

[Index(nameof(ShortName), IsUnique = true)]
public class Discipline : Entity<Guid>
{
    [MaxLength(64), JsonProperty("name")]
    public string Name { get; set; } = Empty;

    [MaxLength(8), JsonProperty("short")]
    public string ShortName { get; set; } = Empty;

    [MaxLength(1024), JsonProperty("description")]
    public string Description { get; set; } = Empty;

    [InverseProperty(nameof(User.Disciplines)), JsonIgnore]
    public IEnumerable<User>? Users { get; init; }

    [NotMapped, JsonIgnore]
    public User? Manager => Users?.FirstOrDefault(u => u.Role == RoleType.CampaignManager);

    [NotMapped, JsonProperty("judgeIds")]
    public IEnumerable<Guid>? JudgeIds => Judges?.Select(j => j.Id);

    [NotMapped, JsonIgnore]
    public IEnumerable<User>? Judges => Users?.Where(u => u.Role == RoleType.CampaignJudge);

    [NotMapped, JsonProperty("managerId")]
    public Guid? ManagerId => Manager?.Id;

    [JsonIgnore]
    public Guid? LocationId { get; init; }
    
    [ForeignKey(nameof(LocationId)), JsonProperty("locationId")]
    public Location? Location { get; set; }

    [InverseProperty(nameof(Entry.Discipline)), JsonIgnore]
    public IEnumerable<Entry>? Entries { get; init; }
    
    [JsonProperty("entryIds")]
    public IEnumerable<Guid>? EntryIds => Entries?.Select(e => e.Id);
}