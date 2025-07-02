using ChatAppAPI.Dto;
using ChatAppAPI.Requests;

namespace ChatAppAPI.Mappers
{
    public static class MessageMapper
    {
        public static MessageDto ToMessageDto(this MessageRequest messageRequest)
        {
            return new MessageDto
            {
                Username = messageRequest.Username,
                Message = messageRequest.Message,
                Type = messageRequest.Type,
            };
        }
    }
}
