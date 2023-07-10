namespace TaskManager.DAL.Models.BaseModels
{
    /// <summary>
    /// Abstract base class for models in the Task Manager application.
    /// </summary>
    public abstract class BaseModel
    {
        /// <summary>
        /// Gets or sets the ID of the model.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the model was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the model was deleted.
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the model was last edited.
        /// </summary>
        public DateTime? EditedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the model is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}