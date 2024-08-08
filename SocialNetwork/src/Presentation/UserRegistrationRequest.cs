using System.ComponentModel.DataAnnotations;

namespace Presentation;

public class UserRegistrationRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}