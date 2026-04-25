namespace Messenger.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public string Login { get; set; } = null!;
    public string HashPassword { get; set; } = null!;
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
}