using Messenger.Domain.Common;

namespace Messenger.Domain.Entities;

public class User : IEntity<Guid>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; private set; } = null!;
    public string? SecondName { get; private set; }
    public string? AvatarUrl { get; private set; }
    
    public Account? Account { get; private set; }

    private readonly List<ChatMember> _chatMembers = [];
    private readonly List<Chat> _ownedChats = [];
    private readonly List<Message> _messages = [];

    public IReadOnlyCollection<ChatMember> ChatMembers => _chatMembers.AsReadOnly();
    public IReadOnlyCollection<Chat> OwnedChats => _ownedChats.AsReadOnly();
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
    
    private User() {}

    public User(string firstName, string? secondName = null, string? avatarUrl = null) : this()
    {
        FirstName = firstName;
        SecondName = secondName;
        AvatarUrl = avatarUrl;
    }
}