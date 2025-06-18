namespace ChatAppAPI.Interface
{
    public interface IChatService
    {
        Task CreateLobby(string connectionId);
        Task JoinLobby(string connectionId);
    }
}
