namespace ChatAppAPI.Dto
{
    public class MessageDto
    {
        public required string GroupId;
        public string Username { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
