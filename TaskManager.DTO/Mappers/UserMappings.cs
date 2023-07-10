using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DTO.DTO_s.User;

namespace TaskManager.DTO.Mappers
{
    public class UserMappings : Profile
    {
        public UserMappings() 
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));
        }
    }
}