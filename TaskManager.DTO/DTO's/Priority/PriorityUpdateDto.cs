using System.ComponentModel.DataAnnotations;
using TaskManager.DAL.Enums;

namespace TaskManager.DTO.DTO_s.Priority
{
    public class PriorityUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority level is required")]
        public PriorityLevel PriorityLevel { get; set; }
    }
}
