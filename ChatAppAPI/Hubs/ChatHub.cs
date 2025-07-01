using ChatAppAPI.Common.ErrorHandling;
using ChatAppAPI.Dto;
using ChatAppAPI.Interface;
using ChatAppAPI.Requests;
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

        public async Task<OperationResult> CreateRoom()
        {
            return await _chatService.CreateRoom(Context.ConnectionId);
        }

        public async Task<OperationResult> JoinRoom(string groupNameGUID)
        {
            return await _chatService.JoinRoom(Context.ConnectionId, groupNameGUID);
        }

        public OperationResult LeaveRoom()
        {
            return _chatService.LeaveRoom(Context.ConnectionId);
        }

        public async Task<OperationResult> SendMessage(MessageRequest message)
        {
            return await _chatService.SendMessage(Context.ConnectionId, message);
        }

        public async Task<OperationResult> NotifyUserJoined(UserJoinedRequest userJoinedRequest)
        {
            return await _chatService.NotifyUserJoined(Context.ConnectionId, userJoinedRequest);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            //Remove disconnected user from Chat room
            _chatService.LeaveRoom(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
