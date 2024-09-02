namespace sf.Server.Models.Auth.Dto;

public class LoginModel
{
    public User? Username { get; set; }
    public string? Password { get; set; }
    
    public Guid? SchoolID { get; set; }
}