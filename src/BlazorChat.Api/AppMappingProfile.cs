using AutoMapper;
using BlazorChat.Api.Database.Entities.Chatting;
using BlazorChat.Shared.Chatting;

namespace BlazorChat.Api;

internal class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<MessageEntity, Message>();
    }
}