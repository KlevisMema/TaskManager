namespace TaskManager.DAL.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<Task>? Tasks { get; set; }
        public List<User>? Users { get; set; }
    }
}