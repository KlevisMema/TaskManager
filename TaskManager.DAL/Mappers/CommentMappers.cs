using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.DTO_s.Comment;

namespace TaskManager.DAL.Mappers
{
    public class CommentMappers : Profile
    {
        public CommentMappers() 
        {
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}