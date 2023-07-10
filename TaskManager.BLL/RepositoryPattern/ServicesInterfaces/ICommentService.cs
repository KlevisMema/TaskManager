/*
This interface defines the contract for managing comments in the Task Manager application.

Methods:
- CreateComment(CommentCreateDto commentDto): Creates a new comment.
- GetCommentsForTask(Guid taskId): Retrieves all comments for a specific task.
- GetCommentById(Guid commentId): Retrieves a specific comment by its ID.
- UpdateComment(Guid commentId, CommentUpdateDto commentDto): Updates an existing comment.
- CreateCommentForTask(Guid taskId, CommentCreateDto commentDto): Creates a new comment for a specific task.
- SoftDeleteComment(Guid commentId): Soft deletes a comment by setting the IsDeleted flag to true.
- HardDeleteComment(Guid commentId): Hard deletes a comment by removing it from the database.
- RestoreSoftDeletedComment(Guid commentId): Restores a soft-deleted comment by setting the IsDeleted flag to false.

Note: All methods return a Task<Response<T>> object, where T represents the return type of the method.
*/

#region Usings
using TaskManager.DAL.DTO_s.Comment;
using TaskManager.HELPERS.ServiceResponse;
#endregion

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    /// <summary>
    /// The interface for managing comments in the Task Manager application.
    /// </summary>
    public interface ICommentService
    {
        #region Methods
        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="commentDto">The <see cref="CommentCreateDto"/> object containing the data for the new comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the created comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the comment.</exception>
        Task<Response<CommentDto>>
        CreateComment
        (
            CommentCreateDto commentDto
        );

        /// <summary>
        /// Retrieves all comments for a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the retrieved comments.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the comments.</exception>
        Task<Response<List<CommentDto>>>
        GetCommentsForTask
        (
            Guid taskId
        );

        /// <summary>
        /// Retrieves a specific comment by its ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the retrieved comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the comment.</exception>
        Task<Response<CommentDto>>
        GetCommentById
        (
            Guid commentId
        );

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to update.</param>
        /// <param name="commentDto">The <see cref="CommentUpdateDto"/> object containing the updated data for the comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the updated comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the comment.</exception>
        Task<Response<CommentDto>>
        UpdateComment
        (
            Guid commentId,
            CommentUpdateDto commentDto
        );

        /// <summary>
        /// Creates a new comment for a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task to associate the comment with.</param>
        /// <param name="commentDto">The <see cref="CommentCreateDto"/> object containing the data for the new comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the created comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the comment.</exception>
        Task<Response<CommentDto>>
        CreateCommentForTask
        (
            Guid taskId,
            CommentCreateDto commentDto
        );

        /// <summary>
        /// Soft deletes a comment by setting the IsDeleted flag to true.
        /// </summary>
        /// <param name="commentId">The ID of the comment to soft delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the comment.</exception>
        Task<Response<bool>>
        SoftDeleteComment
        (
            Guid commentId
        );

        /// <summary>
        /// Hard deletes a comment by removing it from the database.
        /// </summary>
        /// <param name="commentId">The ID of the comment to hard delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the comment.</exception>
        Task<Response<bool>>
        HardDeleteComment
        (
            Guid commentId
        );

        /// <summary>
        /// Restores a soft-deleted comment by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="commentId">The ID of the comment to restore.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the comment.</exception>
        Task<Response<CommentDto>>
        RestoreSoftDeletedComment
        (
            Guid commentId
        ); 
        #endregion
    }
}