namespace sf.Server.Models.SF;

public class CampaignManager : User, ISchoolReference
{
    [JsonProperty("schoolId")]
    public new Guid SchoolId { get; set; }
    
    [ForeignKey(nameof(SchoolId)), JsonIgnore]
    public new School? School { get; set; }
    
    
}