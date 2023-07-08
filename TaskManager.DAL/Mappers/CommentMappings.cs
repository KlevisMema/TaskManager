using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.DTO_s.Comment;

namespace TaskManager.DAL.Mappers
{
    public class CommentMappings : Profile
    {
        public CommentMappings() 
        {
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<Comment, CommentDto>();
        }
    }
}