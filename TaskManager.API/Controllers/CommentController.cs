/*
    This controller is responsible for managing comments in the Task Manager application.
    It provides endpoints for retrieving, creating, updating, and deleting comments.

    The CommentController inherits from the BaseController, which is a base API controller
    configured with the route prefix "api/[controller]". It also includes the necessary dependencies
    for accessing the comment service.

    The controller includes the following endpoints:

    - GetCommentsForTask: Retrieves all comments for a task.
    - GetCommentById: Retrieves a comment by its ID.
    - CreateCommentForTask: Creates a new comment for a task.
    - UpdateComment: Updates an existing comment by its ID.
    - RestoreSoftDeletedComment: Restores a soft-deleted comment by its ID.
    - SoftDeleteComment: Soft deletes a comment by its ID.
    - HardDeleteComment: Hard deletes a comment by its ID.
*/

#region Usings
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO.DTO_s.Comment;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;
using TaskManager.HELPERS.ServiceResponse;
#endregion

namespace TaskManager.API.Controllers
{
    /// <summary>
    ///     This controller is responsible for managing comments in the Task Manager application.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : BaseController
    {
        /// <summary>
        /// The comment service used for performing comment-related operations.
        /// </summary>
        private readonly ICommentService _commentService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="commentService">The comment service.</param>
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Retrieves all comments for a task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<Response<List<CommentDto>>>>
        GetCommentsForTask
        (
            Guid taskId
        )
        {
            var response = await _commentService.GetCommentsForTask(taskId);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<CommentDto>>>
        GetCommentById
        (
            Guid id
        )
        {
            var response = await _commentService.GetCommentById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new comment for a task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <param name="commentDto">The DTO containing the comment data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost("task/{taskId}")]
        public async Task<ActionResult<Response<CommentDto>>>
        CreateCommentForTask
        (
            Guid taskId,
            CommentCreateDto commentDto
        )
        {
            var response = await _commentService.CreateCommentForTask(taskId, commentDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="commentDto">The DTO containing the updated comment data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<CommentDto>>>
        UpdateComment
        (
            Guid id,
            CommentUpdateDto commentDto
        )
        {
            var response = await _commentService.UpdateComment(id, commentDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Restores a soft-deleted comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to restore.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut("RestoreSoftDeletedComment/{id}")]
        public async Task<ActionResult<Response<CommentDto>>>
        RestoreSoftDeletedComment
        (
            Guid id
        )
        {
            var response = await _commentService.RestoreSoftDeletedComment(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Soft deletes a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("SoftDeleteComment/{id}")]
        public async Task<ActionResult<Response<bool>>>
        SoftDeleteComment
        (
            Guid id
        )
        {
            var response = await _commentService.SoftDeleteComment(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Hard deletes a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete("HardDeleteComment/{id}")]
        public async Task<ActionResult<Response<bool>>>
        HardDeleteComment
        (
            Guid id
        )
        {
            var response = await _commentService.HardDeleteComment(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}