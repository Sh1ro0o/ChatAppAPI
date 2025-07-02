using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ChatAppAPI.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageType
    {
        [EnumMember(Value = "System")]
        System,

        [EnumMember(Value = "User")]
        User,
    }
}
