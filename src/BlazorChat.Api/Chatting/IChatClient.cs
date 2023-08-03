using BlazorChat.Shared.Chatting;

namespace BlazorChat.Api.Chatting;

public interface IChatClient
{
    public Task NowOnlineChanged(int nowOnline);
    public Task ReceiveMessage(Message message);
}