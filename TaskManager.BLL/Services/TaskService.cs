using AutoMapper;
using TaskManager.DAL.Context;
using TaskManager.DAL.DTO_s.Task;
using TaskManager.BLL.BaseServices;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.ServicesInterfaces;
using Task = TaskManager.DAL.Models.Task;

namespace TaskManager.BLL.Services
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService
        (
            IMapper mapper,
            ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {}

        public async Task<Response<TaskDto>>
        CreateTask
        (
            TaskCreateDto taskDto
        )
        {
            try
            {
                var task = _mapper.Map<Task>(taskDto);

                _dbContext.Tasks.Add(task);
                await _dbContext.SaveChangesAsync();

                return Response<TaskDto>.Ok(_mapper.Map<TaskDto>(task), "Task created succsessfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<TaskDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<List<TaskDto>>>
        GetTasks()
        {
            try
            {
                var tasks = await _dbContext.Tasks.ToListAsync();
                return Response<List<TaskDto>>.Ok(_mapper.Map<List<TaskDto>>(tasks), "Tasks retrieved succsessfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<List<TaskDto>>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<TaskDto>>
        GetTaskById
        (
            Guid id
        )
        {
            try
            {
                var task = await _dbContext.Tasks.FindAsync(id);
                if (task == null)
                    return Response<TaskDto>.NotFound($"Task with id: {id} doesn't exists.");

                return Response<TaskDto>.Ok(_mapper.Map<TaskDto>(task), $"Tasks {task.Title} retrieved succsessfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<TaskDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<TaskDto>>
        UpdateTask
        (
            Guid id,
            TaskUpdateDto taskDto
        )
        {
            try
            {
                var task = await _dbContext.Tasks.FindAsync(id);
                if (task == null)
                    return Response<TaskDto>.NotFound($"Task with id: {id} doesn't exists.");

                _mapper.Map(taskDto, task);
                await _dbContext.SaveChangesAsync();

                return Response<TaskDto>.Ok(_mapper.Map<TaskDto>(task), $"Tasks with id: {id} updated succsessfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<TaskDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>>
        DeleteTask
        (
            Guid id
        )
        {
            try
            {
                var task = await _dbContext.Tasks.FindAsync(id);
                if (task == null)
                    return Response<bool>.NotFound($"Task with id: {id} doesn't exists.");

                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Task with id: {id} deleted succsessfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }
    }
}