using Microsoft.AspNetCore.Identity;

namespace TaskManager.DAL.Models.BaseModels
{
    /// <summary>
    /// Represents the base identity model with common properties for user entities.
    /// </summary>
    public abstract class BaseIdentity : IdentityUser
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was soft deleted.
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last edited.
        /// </summary>
        public DateTime? EditedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}