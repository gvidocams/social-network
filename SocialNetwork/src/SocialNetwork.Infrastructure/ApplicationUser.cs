using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Infrastructure;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string? MiddleName { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> Interests { get; set; }
}