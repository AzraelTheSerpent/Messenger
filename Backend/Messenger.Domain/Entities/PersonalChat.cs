using System.Security.Cryptography;

namespace Messenger.Domain.Entities;

public class PersonalChat : Chat
{
    // ReSharper disable once UnusedMember.Local
    private PersonalChat() { }

    public PersonalChat(Guid user1Id, Guid user2Id)
    {
        if (user1Id == user2Id)
            throw new ArgumentException("Cannot create a personal chat with oneself.");

        Id = GenerateDeterministicId(user1Id, user2Id);
        
        AddMember(new ChatMember(user1Id, Id));
        AddMember(new ChatMember(user2Id, Id));
    }

    private static Guid GenerateDeterministicId(Guid user1Id, Guid user2Id)
    {
        var bytes1 = user1Id.ToByteArray();
        var bytes2 = user2Id.ToByteArray();

        var combined = new byte[32];

        if (user1Id.CompareTo(user2Id) < 0)
        {
            Buffer.BlockCopy(bytes1, 0, combined, 0, 16);
            Buffer.BlockCopy(bytes2, 0, combined, 16, 16);
        }
        else
        {
            Buffer.BlockCopy(bytes2, 0, combined, 0, 16);
            Buffer.BlockCopy(bytes1, 0, combined, 16, 16);
        }

        var hashOutput = new byte[16];

        Shake128.HashData(combined, hashOutput);

        return new Guid(hashOutput);
    }
}