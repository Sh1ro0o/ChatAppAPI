using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatAppAPI.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageType
    {
        [EnumMember(Value = "system")]
        System,

        [EnumMember(Value = "user")]
        User,
    }
}
