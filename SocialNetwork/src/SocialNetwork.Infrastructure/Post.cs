namespace SocialNetwork.Infrastructure;

public class Post
{
    public int Id { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string Subject { get; set; }
}