using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.DTO_s.Category
{
    /// <summary>
    /// Data transfer object for creating a new category.
    /// </summary>
    public class CategoryCreateDto
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;
    }
}