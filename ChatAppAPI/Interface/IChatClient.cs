using ChatAppAPI.Dto;

namespace ChatAppAPI.Interface
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageDto message);
    }
}
