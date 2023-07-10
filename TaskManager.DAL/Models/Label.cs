using TaskManager.DAL.Models.BaseModels;

namespace TaskManager.DAL.Models
{
    /// <summary>
    /// Represents a label entity.
    /// </summary>
    public class Label : BaseModel
    {
        /// <summary>
        /// Gets or sets the name of the label.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of tasks associated with the label.
        /// </summary>
        public List<Task>? Tasks { get; set; }
    }
}