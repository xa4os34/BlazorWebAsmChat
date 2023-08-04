namespace BlazorChat.Shared.Chatting;

public interface IChatHub 
{
    public int GetNowOnline();

    public IEnumerable<Message> GetMessages();

    public SentMessage(string message);
}