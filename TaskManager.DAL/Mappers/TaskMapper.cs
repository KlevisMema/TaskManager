using AutoMapper;
using TaskManager.DAL.DTO_s.Task;

namespace TaskManager.DAL.Mappers
{
    public class TaskMapper : Profile
    {
        public TaskMapper()
        {
            CreateMap<Models.Task, TaskDto>();
            CreateMap<TaskCreateDto, Models.Task>();
        }
    }
}