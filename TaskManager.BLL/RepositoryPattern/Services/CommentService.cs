/*
CommentService class is responsible for managing comments in the Task Manager application.
It uses AutoMapper for object mapping and interacts with the data access layer (DAL) through the ApplicationDbContext.
The CommentService implements the ICommentService interface.

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
using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.DTO.DTO_s.Comment;
using TaskManager.HELPERS.LogsHelper;
using TaskManager.HELPERS.ServiceResponse;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
#endregion

namespace TaskManager.BLL.RepositoryPattern.Services
{
    /// <summary>
    /// CommentService class is responsible for managing comments in the Task Manager application.
    /// </summary>
    public class CommentService : BaseService, ICommentService
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="mapper">The <see cref="IMapper"/> instance.</param>
        /// <param name="dbContext">The <see cref="ApplicationDbContext"/> instance.</param>
        public CommentService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="commentDto">The <see cref="CommentCreateDto"/> object containing the data for the new comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the created comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the comment.</exception>
        public async Task<Response<CommentDto>>
        CreateComment
        (
            CommentCreateDto commentDto
        )
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDto);

                _dbContext.Comments.Add(comment);
                await _dbContext.SaveChangesAsync();

                return Response<CommentDto>.Ok(_mapper.Map<CommentDto>(comment), "Comment created successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves all comments for a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the retrieved comments.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the comments.</exception>
        public async Task<Response<List<CommentDto>>>
        GetCommentsForTask
        (
            Guid taskId
        )
        {
            try
            {
                var comments = await _dbContext.Comments
                    .Where(c => c.TaskId == taskId)
                    .ToListAsync();

                return Response<List<CommentDto>>.Ok(_mapper.Map<List<CommentDto>>(comments), "Comments retrieved successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<List<CommentDto>>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Retrieves a specific comment by its ID.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the retrieved comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while retrieving the comment.</exception>
        public async Task<Response<CommentDto>>
        GetCommentById
        (
            Guid commentId
        )
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(commentId);
                if (comment == null)
                    return Response<CommentDto>.NotFound($"Comment with id: {commentId} doesn't exist.");

                return Response<CommentDto>.Ok(_mapper.Map<CommentDto>(comment), $"Comment {comment.Id} retrieved successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Updates an existing comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to update.</param>
        /// <param name="commentDto">The <see cref="CommentUpdateDto"/> object containing the updated data for the comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the updated comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while updating the comment.</exception>
        public async Task<Response<CommentDto>>
        UpdateComment
        (
            Guid commentId,
            CommentUpdateDto commentDto
        )
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(commentId);
                if (comment == null)
                    return Response<CommentDto>.NotFound($"Comment with id: {commentId} doesn't exist.");

                _mapper.Map(commentDto, comment);
                await _dbContext.SaveChangesAsync();

                return Response<CommentDto>.Ok(_mapper.Map<CommentDto>(comment), $"Comment with id: {commentId} updated successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Creates a new comment for a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task to associate the comment with.</param>
        /// <param name="commentDto">The <see cref="CommentCreateDto"/> object containing the data for the new comment.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object containing the created comment.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while creating the comment.</exception>
        public async Task<Response<CommentDto>>
        CreateCommentForTask
        (
            Guid taskId,
            CommentCreateDto commentDto
        )
        {
            try
            {
                var comment = _mapper.Map<Comment>(commentDto);
                comment.TaskId = taskId;

                _dbContext.Comments.Add(comment);
                await _dbContext.SaveChangesAsync();

                return Response<CommentDto>.Ok(_mapper.Map<CommentDto>(comment), "Comment created successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Soft deletes a comment by setting the IsDeleted flag to true.
        /// </summary>
        /// <param name="commentId">The ID of the comment to soft delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while soft deleting the comment.</exception>
        public async Task<Response<bool>>
        SoftDeleteComment
        (
            Guid commentId
        )
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(commentId);
                if (comment == null)
                    return Response<bool>.NotFound($"Comment with id: {commentId} doesn't exist.");

                comment.IsDeleted = true;
                comment.DeletedAt = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Comment with id: {commentId} soft deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Hard deletes a comment by removing it from the database.
        /// </summary>
        /// <param name="commentId">The ID of the comment to hard delete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while hard deleting the comment.</exception>
        public async Task<Response<bool>>
        HardDeleteComment
        (
            Guid commentId
        )
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(commentId);
                if (comment == null)
                    return Response<bool>.NotFound($"Comment with id: {commentId} doesn't exist.");

                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Comment with id: {commentId} hard deleted successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

        /// <summary>
        /// Restores a soft-deleted comment by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="commentId">The ID of the comment to restore.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a <see cref="Response{T}"/> object indicating the success of the operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while restoring the comment.</exception>
        public async Task<Response<CommentDto>>
        RestoreSoftDeletedComment
        (
            Guid commentId
        )
        {
            try
            {
                var comment = await _dbContext.Comments.FindAsync(commentId);
                if (comment == null)
                    return Response<CommentDto>.NotFound($"Comment with id: {commentId} doesn't exist.");

                if (!comment.IsDeleted)
                    return Response<CommentDto>.UnSuccessMessage($"Comment with id: {commentId} is already active");

                comment.IsDeleted = false;
                comment.EditedAt = DateTime.Now;
                await _dbContext.SaveChangesAsync();

                return Response<CommentDto>.Ok(_mapper.Map<CommentDto>(comment), $"Comment with id: {commentId} restored successfully");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }
        #endregion
    }
}