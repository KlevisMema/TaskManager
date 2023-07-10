/*
    This interface defines the contract for managing labels.
    It provides methods for retrieving, creating, updating, and deleting labels.
    Each method returns a response object containing the result and any associated data or error messages.

    - GetLabels: Retrieves a list of all labels.
    - GetLabelById: Retrieves a label by its ID.
    - CreateLabel: Creates a new label.
    - UpdateLabel: Updates an existing label.
    - SoftDeleteLabel: Soft deletes a label by its ID.
    - HardDeleteLabel: Hard deletes a label by its ID.
    - RestoreLabel: Restores a soft-deleted label by its ID.
*/

#region Usings
using TaskManager.DAL.DTO_s.Label;
using TaskManager.HELPERS.ServiceResponse;
#endregion

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    /// <summary>
    /// Interface for managing labels.
    /// </summary>
    public interface ILabelService
    {
        #region Methods
        /// <summary>
        /// Retrieves a list of all labels.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the list of labels.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the labels.</exception>
        Task<Response<List<LabelDto>>>
        GetLabels();

        /// <summary>
        /// Creates a new label.
        /// </summary>
        /// <param name="labelDto">The DTO containing the label data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the created label.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the label.</exception>
        Task<Response<LabelDto>>
        CreateLabel
        (
            LabelCreateDto labelDto
        );

        /// <summary>
        /// Retrieves a label by its ID.
        /// </summary>
        /// <param name="labelId">The ID of the label.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the label.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the label.</exception>
        Task<Response<LabelDto>>
        GetLabelById
        (
            Guid labelId
        );

        /// <summary>
        /// Updates an existing label.
        /// </summary>
        /// <param name="labelId">The ID of the label to update.</param>
        /// <param name="labelDto">The DTO containing the updated label data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the updated label.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the label.</exception>
        Task<Response<LabelDto>>
        UpdateLabel
        (
            Guid labelId,
            LabelUpdateDto labelDto
        );

        /// <summary>
        /// Restores a soft-deleted label.
        /// </summary>
        /// <param name="labelId">The ID of the label to restore.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the restored label.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the label.</exception>
        Task<Response<LabelDto>>
        RestoreSoftDeletedLabel
        (
            Guid labelId
        );

        /// <summary>
        /// Soft deletes a label.
        /// </summary>
        /// <param name="labelId">The ID of the label to soft delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the deletion status.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the label.</exception>
        Task<Response<bool>>
        SoftDeleteLabel
        (
            Guid labelId
        );

        /// <summary>
        /// Hard deletes a label.
        /// </summary>
        /// <param name="labelId">The ID of the label to hard delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a response containing the deletion status.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the label.</exception>
        Task<Response<bool>>
        HardDeleteLabel
        (
            Guid labelId
        );
        #endregion
    }
}