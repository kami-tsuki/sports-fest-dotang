namespace sf.Server.Models.SF;

public class DisciplineModel : Entity<Guid>
{
    [JsonProperty(nameof(Name)), MaxLength(50), Required]
    public string Name { get; set; } = Empty;
    
    [JsonIgnore, ForeignKey(nameof(TeamModel.DisciplineId))]
    public ICollection<TeamModel> Teams { get; set; } = new List<TeamModel>();
    
    [JsonProperty(nameof(Students))]
    public ICollection<UserModel> Students => Teams.SelectMany(c => c.Users).ToList();
    
}