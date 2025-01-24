namespace sf.Server.Models.SF;

public class ClassModel : Entity<Guid>
{
    [JsonProperty(nameof(Name)), MaxLength(50), Required]
    public string Name { get; set; } = Empty;
    
    [JsonIgnore, ForeignKey(nameof(UserModel.ClassId))]
    public ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    
    [JsonProperty(nameof(Students)), NotMapped]
    public ICollection<UserModel> Students => Users.Where(u => u.Role == RoleType.Student).ToList();
    
    [JsonProperty(nameof(Tutors)), NotMapped]
    public ICollection<UserModel> Tutors => Users.Where(u => u.Role == RoleType.Tutor).ToList();
}