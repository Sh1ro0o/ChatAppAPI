namespace ChatAppAPI.Interface
{
    public interface IRoomStoreService
    {
        public bool RoomExists(string roomName);
        public bool AddToRoom(string connectionId, string roomName);
        public bool RemoveFromRoom(string connectionId, string roomName);
    }
}
