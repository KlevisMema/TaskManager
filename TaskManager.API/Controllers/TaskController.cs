using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO.DTO_s.Task;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves all tasks.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var response = await _taskService.GetTasks();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var response = await _taskService.GetTaskById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="taskDto">The DTO containing the task data.</param>
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateDto taskDto)
        {
            var response = await _taskService.CreateTask(taskDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="taskDto">The DTO containing the updated task data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, TaskUpdateDto taskDto)
        {
            var response = await _taskService.UpdateTask(id, taskDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var response = await _taskService.DeleteTask(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}