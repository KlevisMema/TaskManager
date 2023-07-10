using TaskManager.DTO.DTO_s.Task;
using TaskManager.DTO.DTO_s.User;

namespace TaskManager.DTO.DTO_s.Project
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<TaskDto>? Tasks { get; set; }
        public List<UserDto>? Users { get; set; }
    }
}