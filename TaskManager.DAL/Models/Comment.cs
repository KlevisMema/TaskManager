using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    public class Comment : BaseModel
    {
        public Guid TaskId { get; set; }
        public Task? Task { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}