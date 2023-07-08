using Microsoft.AspNetCore.Mvc;
using TaskManager.DAL.DTO_s.Comment;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Retrieves all comments for a task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetCommentsForTask(Guid taskId)
        {
            var response = await _commentService.GetCommentsForTask(taskId);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var response = await _commentService.GetCommentById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new comment for a task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <param name="commentDto">The DTO containing the comment data.</param>
        [HttpPost("task/{taskId}")]
        public async Task<IActionResult> CreateCommentForTask(Guid taskId, CommentCreateDto commentDto)
        {
            var response = await _commentService.CreateCommentForTask(taskId, commentDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="commentDto">The DTO containing the updated comment data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, CommentUpdateDto commentDto)
        {
            var response = await _commentService.UpdateComment(id, commentDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Deletes a comment by its ID.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var response = await _commentService.DeleteComment(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}