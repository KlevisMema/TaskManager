using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTO.DTO_s.Category
{
    /// <summary>
    /// Data transfer object for updating a category.
    /// </summary>
    public class CategoryUpdateDto
    {
        /// <summary>
        /// Gets or sets the updated name of the category.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}