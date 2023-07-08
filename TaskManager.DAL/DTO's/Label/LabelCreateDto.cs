using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.DTO_s.Label
{
    public class LabelCreateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}