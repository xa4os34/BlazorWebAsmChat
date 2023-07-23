namespace BlazorChat.Api.Database.Entities.Chatting;

public class MessageEntity : IDatedEntity 
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; } = null;

    public string Text { get; set; } = string.Empty;
}