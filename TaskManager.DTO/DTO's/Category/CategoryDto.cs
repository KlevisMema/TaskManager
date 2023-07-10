namespace TaskManager.DTO.DTO_s.Category
{
    /// <summary>
    /// Data transfer object for the category.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Gets or sets the ID of the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the category is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}