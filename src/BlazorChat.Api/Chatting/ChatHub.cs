using BlazorChat.Api.Database;
using BlazorChat.Api.Database.Entities.Chatting;
using BlazorChat.Shared.Chatting;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;

namespace BlazorChat.Api.Chatting;

public class ChatHub : IChatHub, Hub<IChatClient>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _dbContext;

    private static readonly object s_lockObject = new();
    private static volatile int s_nowOnline = 0;

    public ChatHub(
        IMapper mapper,
        ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public override Task OnConnectedAsync()
    {
        lock (s_lockObject)
            s_nowOnline += 1;

        Clients.All.NowOnlineChanged(s_nowOnline);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        lock (s_lockObject)
            s_nowOnline -= 1;

        Clients.All.NowOnlineChanged(s_nowOnline);

        return base.OnDisconnectedAsync(exception);
    }

    public int GetNowOnline()
    {
        return s_nowOnline;
    }

    public IEnumerable<Message> GetMessages()
    {
        return _mapper
            .ProjectTo<Message>(
                _dbContext.Messages
                    .OrderByDescending(x => x.CreatedAt))
            .ToArray();
    }

    public async Task<Message> SendMessage(string messageText)
    {
        var messageEntity = new MessageEntity
        {
            Text = messageText
        };

        _dbContext.Messages.Add(messageEntity);

        await _dbContext.SaveChangesAsync();

        Message message = _mapper.Map<Message>(messageEntity);

        await Clients.All.ReceiveMessage(message);

        return message;
    }
}