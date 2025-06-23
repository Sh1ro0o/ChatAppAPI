using ChatAppAPI.Common.ErrorHandling;
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
        private readonly IRoomStoreService _roomStoreService;

        public ChatService(IHubContext<ChatHub> hubContext, IRoomStoreService roomStoreService)
        {
            _hubContext = hubContext;
            _roomStoreService = roomStoreService;
        }

        public async Task<OperationResult> CreateRoom(string connectionId)
        {
            string groupNameGUID = Guid.NewGuid().ToString();

            try
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, groupNameGUID);
                _roomStoreService.AddToRoom(connectionId, groupNameGUID);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error creating to group: {ex.Message}");
            }

            return OperationResult<string>.Success(groupNameGUID);
        }

        public async Task<OperationResult> JoinRoom(string connectionId, string groupNameGUID)
        {
            if (!_roomStoreService.RoomExists(groupNameGUID))
            {
                return OperationResult.Failure($"Error joining to Room");
            }

            if (_roomStoreService.UserInRoomExists(connectionId, groupNameGUID))
            {
                return OperationResult<string>.Success(groupNameGUID);
            }

            try
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, groupNameGUID);
                _roomStoreService.AddToRoom(connectionId, groupNameGUID);

                return OperationResult<string>.Success(groupNameGUID);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error joining to Room: {ex.Message}");
            }
        }
    }
}
