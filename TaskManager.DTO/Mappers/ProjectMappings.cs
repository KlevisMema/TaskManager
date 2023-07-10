using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DTO.DTO_s.Project;

namespace TaskManager.DTO.Mappers
{
    public class ProjectMappings : Profile
    {
        public ProjectMappings()
        {
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));
        }
    }
}