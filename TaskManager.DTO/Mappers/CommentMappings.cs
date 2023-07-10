/*
    This class is responsible for configuring AutoMapper mappings for the Comment entity.
    It defines the mappings between CommentCreateDto, CommentUpdateDto, and CommentDto objects to the Comment entity.

    AutoMapper is used to automatically map properties from the source object to the destination object.

    - CreateMap<CommentCreateDto, Comment>:
        Maps the properties from CommentCreateDto to Comment entity.
        Sets the CreatedAt property of the Comment entity to the current UTC date and time.

    - CreateMap<CommentUpdateDto, Comment>:
        Maps the properties from CommentUpdateDto to Comment entity.
        Sets the EditedAt property of the Comment entity to the current UTC date and time.

    - CreateMap<Comment, CommentDto>:
        Maps the properties from Comment entity to CommentDto.
*/

#region Usings
using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DTO.DTO_s.Comment;
#endregion

namespace TaskManager.DTO.Mappers
{
    /// <summary>
    /// The class responsible for configuring AutoMapper mappings for the Comment entity.
    /// </summary>
    public class CommentMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentMappings"/> class.
        /// </summary>
        public CommentMappings()
        {
            // Maps CommentCreateDto to Comment entity
            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Maps CommentUpdateDto to Comment entity
            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.EditedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Maps Comment entity to CommentDto
            CreateMap<Comment, CommentDto>();
        }
    }
}