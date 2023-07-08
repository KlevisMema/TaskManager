using TaskManager.BLL.ServiceResponse;
using TaskManager.DAL.DTO_s.Comment;

namespace TaskManager.BLL.ServicesInterfaces
{
    public interface ICommentService
    {
        Task<Response<CommentDto>> CreateComment(CommentCreateDto commentDto);
        Task<Response<List<CommentDto>>> GetCommentsForTask(Guid taskId);
        Task<Response<CommentDto>> GetCommentById(Guid commentId);
        Task<Response<CommentDto>> UpdateComment(Guid commentId, CommentUpdateDto commentDto);
        Task<Response<bool>> DeleteComment(Guid commentId);
    }
}