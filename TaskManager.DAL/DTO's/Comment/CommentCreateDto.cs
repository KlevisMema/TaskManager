namespace TaskManager.DAL.DTO_s.Comment
{
    /// <summary>
    /// Data Transfer Object for creating a comment.
    /// </summary>
    public class CommentCreateDto
    {
        /// <summary>
        /// Gets or sets the ID of the task associated with the comment.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Gets or sets the text of the comment.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}