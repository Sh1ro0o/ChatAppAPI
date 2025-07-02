using ChatAppAPI.Common.Enums;

namespace ChatAppAPI.Dto
{
    public class MessageDto
    {
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public MessageType Type { get; set; }
    }
}
