using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.DTO_s.Project
{
    public class ProjectCreateDto
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}