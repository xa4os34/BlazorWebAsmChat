namespace BlazorChat.Shared.Chatting;

public class Message
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; } = null;
    
    public bool IsModifiedAt => ModifiedAt is not null;

    public string Text { get; set; } = string.Empty;
}