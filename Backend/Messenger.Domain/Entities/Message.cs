using Messenger.Domain.Common;

namespace Messenger.Domain.Entities;

public class Message : IEntity<long>
{
    public long Id { get; init; }
    public Guid? SenderId { get; private set; } 
    public Guid ChatId { get; private set; } 
    public string? Text { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow; 

    public User? Sender { get; private set; }
    public Chat Chat { get; private set; } = null!; 
    
    private readonly List<MessageAttachment> _attachments = [];
    public IReadOnlyCollection<MessageAttachment> Attachments => _attachments.AsReadOnly();
    // ReSharper disable once UnusedMember.Local
    private Message(){}
    public Message(User sender, Chat chat, string? text, List<MessageAttachment>? attachments)
    {
        if (string.IsNullOrWhiteSpace(text) && attachments == null)
            throw new ArgumentException("Either text or attachments must be provided");
        
        Sender = sender;
        Chat = chat;
        SenderId = sender.Id;
        ChatId = chat.Id;
        Text = text;
        _attachments = attachments ?? [];
        
        chat.AddMessage(this);
    }
}