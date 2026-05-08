using Messenger.Domain.Common;

namespace Messenger.Domain.Entities;

public class Account : IEntity<Guid>
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Login { get; private set; } = null!;
    public string HashPassword { get; private set; } = null!;
    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;

    // ReSharper disable once UnusedMember.Local
    private Account(){}
    public Account(string login, string hashPassword, User user)
    {
        Login = login;
        HashPassword = hashPassword;
        UserId = user.Id;
        User = user;
    }
}