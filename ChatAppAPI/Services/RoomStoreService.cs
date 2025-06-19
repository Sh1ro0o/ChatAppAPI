using ChatAppAPI.Interface;
using System.Collections.Concurrent;

namespace ChatAppAPI.Services
{
    public class RoomStoreService : IRoomStoreService
    {
        private ConcurrentDictionary<string, string> Rooms = new ConcurrentDictionary<string, string>();

        public bool RoomExists(string roomName)
        {
            return Rooms.Any(x => x.Value == roomName);
        }

        public bool AddToRoom(string connectionId, string roomName)
        {
            var result = Rooms.TryAdd(connectionId, roomName);

            return result;
        }

        public bool RemoveFromRoom(string connectionId, string roomName)
        {
            //If Room exists remove
            if (Rooms.TryGetValue(connectionId, out var existingRoomName) && roomName == existingRoomName)
            {
                return Rooms.TryRemove(connectionId, out _);
            }

            return false;
        }
    }
}
