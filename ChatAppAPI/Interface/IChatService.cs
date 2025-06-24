using ChatAppAPI.Common.ErrorHandling;
using ChatAppAPI.Dto;
using ChatAppAPI.Requests;

namespace ChatAppAPI.Interface
{
    public interface IChatService
    {
        Task<OperationResult> CreateRoom(string connectionId);
        Task<OperationResult> JoinRoom(string connectionId, string groupNameGUID);
        Task SendMessage(string connectionId, MessageRequest message);
    }
}
