namespace ChatAppAPI.Interface
{
    public interface IRoomStoreService
    {
        bool RoomExists(string roomName);
        bool UserInRoomExists(string connectionId, string roomName);
        bool AddToRoom(string connectionId, string roomName);
        bool RemoveFromRoom(string connectionId, string? roomName = null);
    }
}
