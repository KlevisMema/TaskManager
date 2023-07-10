using System.Text.Json.Serialization;

namespace TaskManager.DAL.Enums
{
    /// <summary>
    /// Represents the status of a task.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskStatus
    {
        /// <summary>
        /// The task has not been started.
        /// </summary>
        NotStarted = 1,

        /// <summary>
        /// The task is in progress.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// The task has been completed.
        /// </summary>
        Completed = 3,

        /// <summary>
        /// The task has been archived.
        /// </summary>
        Archived = 4
    }
}