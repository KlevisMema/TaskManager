using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    /// <summary>
    /// Represents a comment entity.
    /// </summary>
    public class Comment : BaseModel
    {
        /// <summary>
        /// Gets or sets the ID of the task associated with the comment.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Gets or sets the task associated with the comment.
        /// </summary>
        public Task? Task { get; set; }

        /// <summary>
        /// Gets or sets the text content of the comment.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}