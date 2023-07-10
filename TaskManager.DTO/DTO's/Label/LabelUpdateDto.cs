using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.DTO_s.Label
{
    /// <summary>
    /// DTO for updating a label.
    /// </summary>
    public class LabelUpdateDto
    {
        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}