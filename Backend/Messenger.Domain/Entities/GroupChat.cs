namespace Messenger.Domain.Entities;

public class GroupChat : Chat
{
    public string Name { get; private set; } = null!;
    public string? ImageUrl { get; private set; }
    public Guid? OwnerId { get; private set; }
    
    public User? Owner { get; private set; }
    // ReSharper disable once UnusedMember.Local
    private GroupChat(){}

    public GroupChat(User? owner, string name, string? imageUrl = null)
    {
        ArgumentNullException.ThrowIfNull(owner);
        Id = Guid.NewGuid();
        Owner = owner;
        OwnerId = owner.Id;
        Name = name;
        ImageUrl = imageUrl;
        AddMember(new ChatMember(owner.Id, Id));
    }
}