using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.DTO_s.Task
{
    public class TaskUpdateDto
    {
        [Required]
        [StringLength(30)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public TaskStatus Status { get; set; }
    }
}