using ChatAppAPI.Common.ErrorHandling;
using ChatAppAPI.Dto;
using ChatAppAPI.Requests;

namespace ChatAppAPI.Interface
{
    public interface IChatService
    {
        Task<OperationResult> CreateRoom(string connectionId);
        Task<OperationResult> JoinRoom(string connectionId, string groupNameGUID);
        OperationResult LeaveRoom(string connectionId);
        Task<OperationResult> SendMessage(string connectionId, MessageRequest message);
        Task OnDisconnected(string connectionId);

    }
}
