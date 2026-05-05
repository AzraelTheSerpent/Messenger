namespace Messenger.Domain.Entities;

public class GroupChat : Chat
{
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public Guid? OwnerId { get; set; }
    
    public User? Owner { get; set; }
}