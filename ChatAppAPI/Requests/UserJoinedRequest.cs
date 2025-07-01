namespace ChatAppAPI.Requests
{
    public class UserJoinedRequest
    {
        public required string GroupName { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
