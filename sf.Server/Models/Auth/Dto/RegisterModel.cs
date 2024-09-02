namespace sf.Server.Models.Auth.Dto;

public class RegisterModel
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string? Schoolname { get; set; }
    public string? Password { get; set; }
}