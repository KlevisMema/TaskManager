using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.DAL.DTO_s.Comment;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.ServicesInterfaces;

namespace TaskManager.BLL.Services
{
    public class CommentService : BaseService, ICommentService
    {
        public CommentService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base( mapper, dbContext)
        {
        }

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
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

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
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<List<CommentDto>>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<CommentDto>> GetCommentById(Guid commentId)
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
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

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
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<CommentDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>>
        DeleteComment
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

                return Response<bool>.Ok(true, $"Comment with id: {commentId} deleted successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }

    }
}