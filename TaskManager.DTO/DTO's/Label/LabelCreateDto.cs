using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.DTO_s.Label
{
    /// <summary>
    /// DTO for creating a new label.
    /// </summary>
    public class LabelCreateDto
    {
        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}