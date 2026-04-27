namespace Messenger.Domain.Entities;

public class ChatMember
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public long? LastReadMessageId { get; set; }

    public User User { get; set; } = null!;
    public Chat Chat { get; set; } = null!;
    public Message? LastReadMessage { get; set; }
}