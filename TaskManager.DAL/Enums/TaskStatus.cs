using System.Text.Json.Serialization;

namespace TaskManager.DAL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Archived
    }
}