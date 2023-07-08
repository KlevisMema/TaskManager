using TaskManager.DAL.Enums;
using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    public class Priority : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public PriorityLevel PriorityLevel { get; set; }
    }
}