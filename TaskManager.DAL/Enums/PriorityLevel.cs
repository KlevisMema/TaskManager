using System.Text.Json.Serialization;

namespace TaskManager.DAL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PriorityLevel
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}