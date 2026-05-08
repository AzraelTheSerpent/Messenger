using Messenger.Domain.Common;

namespace Messenger.Domain.Entities;

public abstract class Chat : IEntity<Guid>
{
    public Guid Id { get; init; }
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    
    private readonly List<ChatMember> _members = [];
    private readonly List<Message> _messages = [];

    public IReadOnlyCollection<ChatMember> Members => _members.AsReadOnly();
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    private void UpdateTimestamp() => UpdatedAt = DateTime.UtcNow;

    public void AddMessage(Message message)
    {
        _messages.Add(message);
        UpdateTimestamp();
    }
    public void AddMember(ChatMember member)
    {
        _members.Add(member);
        UpdateTimestamp();
    }
}