using ChatAppAPI.Common.ErrorHandling;

namespace ChatAppAPI.Interface
{
    public interface IChatService
    {
        Task<OperationResult> CreateRoom(string connectionId);
        Task<OperationResult> JoinRoom(string connectionId, string groupNameGUID);
    }
}
