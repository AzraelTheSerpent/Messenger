using Messenger.Domain.Common;

namespace Messenger.Domain.Entities;

public class MessageAttachment : IEntity<long>
{
    public long Id { get; init; } 
    public long MessageId { get; private set; } 
    public string FileUrl { get; private set; } = null!; 
    public string FileType { get; private set; } = null!; 
    public int FileSize { get; private set; } 

    public Message Message { get; private set; } = null!;

    private MessageAttachment(){}
    public MessageAttachment(string fileUrl, string fileType, int fileSize)
    {
        FileUrl = fileUrl;
        FileType = fileType;
        FileSize = fileSize;
    }
}