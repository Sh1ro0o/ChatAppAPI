using ChatAppAPI.Hubs;
using ChatAppAPI.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatAppAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task CreateLobby(string connectionId)
        {
            string groupNameGUID = Guid.NewGuid().ToString();

            await _hubContext.Groups.AddToGroupAsync(connectionId, groupNameGUID);
        }

        public async Task JoinLobby(string connectionId)
        {
            string groupNameGUID = Guid.NewGuid().ToString();

            await _hubContext.Groups.AddToGroupAsync(connectionId, groupNameGUID);
        }
    }
}
