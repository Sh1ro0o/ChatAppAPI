using ChatAppAPI.Hubs;
using ChatAppAPI.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatAppAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        //local room storage for temporary rooms
        private ConcurrentQueue<string> knownRooms = new();

        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task CreateLobby(string connectionId)
        {
            string groupNameGUID = Guid.NewGuid().ToString();

            try
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, groupNameGUID);
                knownRooms.Enqueue(groupNameGUID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating to group: {ex.Message}");
            }
        }

        public async Task JoinLobby(string connectionId, string groupNameGUID)
        {
            if (knownRooms.Contains(groupNameGUID))
            {
                try
                {
                    await _hubContext.Groups.AddToGroupAsync(connectionId, groupNameGUID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error joining to group: {ex.Message}");
                }
            }
        }
    }
}
