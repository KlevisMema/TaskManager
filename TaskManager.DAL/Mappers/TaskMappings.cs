using AutoMapper;
using TaskManager.DAL.DTO_s.Task;

namespace TaskManager.DAL.Mappers
{
    public class TaskMappings : Profile
    {
        public TaskMappings()
        {
            CreateMap<Models.Task, TaskDto>();
            CreateMap<TaskCreateDto, Models.Task>();
        }
    }
}