namespace BlazorChat.Shared.Chatting;

public interface IChatClient
{
    public Task NowOnlineChanged(int nowOnline);
    public Task ReceiveMessage(Message message);
}