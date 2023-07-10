using TaskManager.DAL.Enums;

namespace TaskManager.DTO.DTO_s.Priority
{
    public class PriorityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PriorityLevel PriorityLevel { get; set; }
    }
}