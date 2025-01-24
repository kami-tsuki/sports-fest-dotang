namespace sf.Server.Models.SF;

public class TeamModel : Entity<Guid>
{
    [JsonProperty(nameof(Name)), MaxLength(50), Required]
    public string Name { get; set; } = Empty;
    
    [JsonProperty("Students"), ForeignKey(nameof(UserModel.TeamId))]
    public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    
    [JsonProperty(nameof(DisciplineId)), ForeignKey(nameof(Id))]
    public Guid DisciplineId { get; set; } = Guid.Empty;
    
    [JsonProperty(nameof(Discipline))]
    public DisciplineModel? Discipline { get; set; } = null!;
    
    [JsonProperty("Points")]
    public int Score { get; set; } = 0;
}