using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public List<Task>? Tasks { get; set; }
    }
}