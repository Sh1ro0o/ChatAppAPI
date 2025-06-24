namespace ChatAppAPI.Requests
{
    public class MessageRequest
    {
        public required string GroupName;
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
