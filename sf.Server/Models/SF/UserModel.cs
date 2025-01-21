namespace sf.Server.Models.SF;

public class UserModel : Entity<Guid>
{
    [JsonProperty(nameof(Password)), MinLength(8), Required]
    public string Password { get; set; } = Empty;
    
    [JsonProperty(nameof(FirstName)), MaxLength(50), Required]
    public string FirstName { get; set; } = Empty;
    
    [JsonProperty(nameof(LastName)), MaxLength(50), Required]
    public string LastName { get; set; } = Empty;
    
    [JsonProperty(nameof(Role)), Required]
    public RoleType Role { get; set; } = RoleType.User;
    
    [JsonProperty(nameof(TeamId)), ForeignKey(nameof(TeamModel.Id))]
    public Guid TeamId { get; set; } = Guid.Empty;
    
    [JsonProperty(nameof(Team))]
    public TeamModel Team { get; set; } = null!;
    
    [JsonProperty(nameof(ClassId)), ForeignKey(nameof(ClassModel.Id))]
    public Guid ClassId { get; set; } = Guid.Empty;
}