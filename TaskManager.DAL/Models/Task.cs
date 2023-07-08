using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    public class Task : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<Comment>? Comments { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid PriorityId { get; set; }
        public Priority? Priority { get; set; }
        public List<Label>? Labels { get; set; }
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}