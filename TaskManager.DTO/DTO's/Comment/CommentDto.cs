namespace TaskManager.DTO.DTO_s.Comment
{
    /// <summary>
    /// Data Transfer Object for a comment.
    /// </summary>
    public class CommentDto
    {
        /// <summary>
        /// Gets or sets the ID of the comment.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the task associated with the comment.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Gets or sets the text of the comment.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the creation date and time of the comment.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}