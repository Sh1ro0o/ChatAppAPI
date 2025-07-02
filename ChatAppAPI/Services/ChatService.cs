using ChatAppAPI.Common.ErrorHandling;
using ChatAppAPI.Dto;
using ChatAppAPI.Hubs;
using ChatAppAPI.Interface;
using ChatAppAPI.Mappers;
using ChatAppAPI.Requests;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly ILogger<ChatService> _logger;
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;
        private readonly IRoomStoreService _roomStoreService;

        public ChatService(IHubContext<ChatHub, IChatClient> hubContext, IRoomStoreService roomStoreService, ILogger<ChatService> logger)
        {
            _logger = logger;
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

        public OperationResult LeaveRoom(string connectionId)
        {
            var isRemoved = _roomStoreService.RemoveFromRoom(connectionId);

            if (isRemoved)
            {
                return OperationResult<bool>.Success(isRemoved);
            }

            return OperationResult.Failure($"Error leaving Room");
        }

        public async Task<OperationResult> SendMessage(string connectionId, MessageRequest message)
        {
            var messageDto = message.ToMessageDto();

            try
            {
                await _hubContext.Clients.GroupExcept(message.GroupName, connectionId).ReceiveMessage(messageDto);
                return OperationResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error sending a message");
            }
        }

        public async Task OnDisconnected(string connectionId)
        {
            string? usersGroup = _roomStoreService.UsersCurrentRoomName(connectionId);
            if (usersGroup == null)
            {
                _logger.LogError($"Failed to get usersGroup for {connectionId}");
                return;
            }

            var messageDto = new MessageDto
            {
                Message = "A User has lost connection.",
                Type = Common.Enums.MessageType.System
            };

            try
            {
                await _hubContext.Clients.GroupExcept(usersGroup, connectionId).ReceiveMessage(messageDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to notify group {usersGroup} about disconnection of connectionId {connectionId}");
            }

            var isRemoved = _roomStoreService.RemoveFromRoom(connectionId);

            if (!isRemoved)
            {
                _logger.LogError($"Failed to remove user from Group: {usersGroup} ConnectionId: {connectionId}");
            }
        }
    }
}
