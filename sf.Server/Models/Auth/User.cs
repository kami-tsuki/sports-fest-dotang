using Microsoft.AspNetCore.Identity;

namespace sf.Server.Models.Auth;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    [MaxLength(256), JsonProperty("first")]
    public string? FirstName { get; set; } = Empty;

    [MaxLength(256), JsonProperty("last")]
    public string? LastName { get; set; } = Empty;

    [JsonIgnore, MaxLength(256)]
    public string RoleString
    {
        get => Role.ToString();
        set => Role = Enum.Parse<RoleType>(value);
    }
    
    [JsonProperty("role"), NotMapped]
    public virtual RoleType Role { get; set; }

    [JsonIgnore, MaxLength(256)]
    public string ApiKey { get; init; } = Empty;
    
    [Timestamp, Required, JsonProperty("created")]
    public DateTime CreatedAt { get; set; }
    
    [Timestamp, Required, JsonProperty("updated")]
    public DateTime UpdatedAt { get; set; }
}

public enum RoleType
{
    Student,
    Tutor,
    CampaignManager,
    CampaignJudge,
    User
}