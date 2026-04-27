namespace Messenger.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}