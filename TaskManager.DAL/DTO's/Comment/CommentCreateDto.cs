namespace TaskManager.DAL.DTO_s.Comment
{
    public class CommentCreateDto
    {
        public Guid TaskId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}