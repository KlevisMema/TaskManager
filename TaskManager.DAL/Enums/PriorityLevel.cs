using System.Text.Json.Serialization;

namespace TaskManager.DAL.Enums
{
    /// <summary>
    /// Represents the priority level of a task.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PriorityLevel
    {
        /// <summary>
        /// The task has a low priority.
        /// </summary>
        Low = 1,

        /// <summary>
        /// The task has a medium priority.
        /// </summary>
        Medium = 2,

        /// <summary>
        /// The task has a high priority.
        /// </summary>
        High = 3
    }
}