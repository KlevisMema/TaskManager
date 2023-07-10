using AutoMapper;
using TaskManager.DTO.DTO_s.Task;

namespace TaskManager.DTO.Mappers
{
    public class TaskMappings : Profile
    {
        public TaskMappings()
        {
            CreateMap<DAL.Models.Task, TaskDto>();
            CreateMap<TaskCreateDto, DAL.Models.Task>();
        }
    }
}