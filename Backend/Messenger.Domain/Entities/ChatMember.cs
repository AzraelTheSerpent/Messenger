namespace Messenger.Domain.Entities;

public class ChatMember
{
    public Guid UserId { get; private set; }
    public Guid ChatId { get; private set; }
    public long? LastReadMessageId { get; private set; }

    public User User { get; private set; } = null!;
    public Chat Chat { get; private set; } = null!;
    public Message? LastReadMessage { get; private set; }
    
    // ReSharper disable once UnusedMember.Local
    private ChatMember(){}
    public ChatMember(Guid userId, Guid chatId)
    {
        UserId = userId;
        ChatId = chatId;
    }
}