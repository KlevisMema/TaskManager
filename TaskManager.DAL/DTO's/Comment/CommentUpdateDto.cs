namespace TaskManager.DAL.DTO_s.Comment
{
    /// <summary>
    /// Data Transfer Object for updating a comment.
    /// </summary>
    public class CommentUpdateDto
    {
        /// <summary>
        /// Gets or sets the text of the comment.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}