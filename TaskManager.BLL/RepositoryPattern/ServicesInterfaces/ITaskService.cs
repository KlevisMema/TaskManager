using TaskManager.DAL.DTO_s.Task;
using TaskManager.HELPERS.ServiceResponse;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface ITaskService
    {
        Task<Response<List<TaskDto>>> GetTasks();
        Task<Response<bool>> DeleteTask(Guid id);
        Task<Response<TaskDto>> GetTaskById(Guid id);
        Task<Response<TaskDto>> CreateTask(TaskCreateDto taskDto);
        Task<Response<TaskDto>> UpdateTask(Guid id, TaskUpdateDto taskDto);
    }
}