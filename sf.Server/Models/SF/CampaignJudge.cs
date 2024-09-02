using User = sf.Server.Models.Auth.User;

namespace sf.Server.Models.SF;

public class CampaignJudge : User, IDisciplineReference, ISchoolReference
{
    
    [JsonProperty("disciplineId")]
    public Guid? DisciplineId { get; set; }
    
    [ForeignKey(nameof(DisciplineId)), JsonIgnore]
    public Discipline? Discipline { get; set; }
    
    [JsonProperty("schoolId")]
    public Guid SchoolId { get; set; }

    [ForeignKey(nameof(SchoolId)), JsonIgnore]
    public School? School { get; set; }
    
}