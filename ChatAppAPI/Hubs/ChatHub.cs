using ChatAppAPI.Dto;
using ChatAppAPI.Interface;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatService _chatService;
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task CreateLobby()
        {
            await _chatService.CreateLobby(Context.ConnectionId);
        }

        public async Task JoinLobby(string groupNameGUID)
        {
            await _chatService.JoinLobby(Context.ConnectionId, groupNameGUID);
        }

        public async Task SendMessage(MessageDto message)
        {
            await Clients.Group(message.GroupId).ReceiveMessage(message);
        }
    }
}
