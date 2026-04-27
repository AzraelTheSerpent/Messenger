namespace Messenger.Domain.Entities;

public class Message
{
    public long Id { get; set; }
    public Guid? SenderId { get; set; } 
    public Guid ChatId { get; set; } 
    public string? Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 

    public User? Sender { get; set; }
    public Chat Chat { get; set; } = null!; 
    public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
}