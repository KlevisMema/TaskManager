using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    /// <summary>
    /// Represents a category in the Task Manager application.
    /// </summary>
    public class Category : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of tasks associated with the category.
        /// </summary>
        public List<Task>? Tasks { get; set; }
    }
}