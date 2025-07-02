using ChatAppAPI.Interface;
using System.Collections.Concurrent;

namespace ChatAppAPI.Services
{
    public class RoomStoreService : IRoomStoreService
    {
        //Key - connectionId, Value - roomName
        private ConcurrentDictionary<string, string> Rooms = new ConcurrentDictionary<string, string>();

        public bool RoomExists(string roomName)
        {
            return Rooms.Any(x => x.Value == roomName);
        }

        public bool UserInRoomExists(string connectionId, string roomName)
        {
            return Rooms.Any(x => x.Key == connectionId && x.Value == roomName);
        }

        public bool AddToRoom(string connectionId, string roomName)
        {
            var result = Rooms.TryAdd(connectionId, roomName);

            return result;
        }

        public bool RemoveFromRoom(string connectionId, string? roomName = null)
        {
            //If Room exists -> remove
            if (!Rooms.TryGetValue(connectionId, out var existingRoomName))
            {
                return false;
            }

            if (roomName != null && roomName != existingRoomName)
            {
                return false;
            }

            return Rooms.TryRemove(connectionId, out _);
        }

        public string? UsersCurrentRoomName(string connectionId)
        {
            Rooms.TryGetValue(connectionId, out var existingRoomName);

            return existingRoomName;
        }
    }
}
