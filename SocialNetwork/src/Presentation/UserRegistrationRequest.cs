namespace Presentation;

public class UserRegistrationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string? MiddleName { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> Interests { get; set; }
}