using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    public class User : BaseIdentity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public List<Task>? Tasks { get; set; }
        public Guid? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}