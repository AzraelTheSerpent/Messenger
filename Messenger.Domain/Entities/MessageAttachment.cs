namespace Messenger.Domain.Entities;

public class MessageAttachment
{
    public long Id { get; set; } 
    public long MessageId { get; set; } 
    public string FileUrl { get; set; } = null!; 
    public string FileType { get; set; } = null!; 
    public int FileSize { get; set; } 

    public Message Message { get; set; } = null!; 
}