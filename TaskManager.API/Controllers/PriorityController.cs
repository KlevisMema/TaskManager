using Microsoft.AspNetCore.Mvc;
using TaskManager.DTO.DTO_s.Priority;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriorityController : BaseController
    {
        private readonly IPriorityService _priorityService;

        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        /// <summary>
        /// Retrieves all priorities.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPriorities()
        {
            var response = await _priorityService.GetPriorities();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a priority by its ID.
        /// </summary>
        /// <param name="id">The ID of the priority.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriorityById(Guid id)
        {
            var response = await _priorityService.GetPriorityById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new priority.
        /// </summary>
        /// <param name="priorityDto">The DTO containing the priority data.</param>
        [HttpPost]
        public async Task<IActionResult> CreatePriority(PriorityCreateDto priorityDto)
        {
            var response = await _priorityService.CreatePriority(priorityDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing priority by its ID.
        /// </summary>
        /// <param name="id">The ID of the priority to update.</param>
        /// <param name="priorityDto">The DTO containing the updated priority data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePriority(Guid id, PriorityUpdateDto priorityDto)
        {
            var response = await _priorityService.UpdatePriority(id, priorityDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Deletes a priority by its ID.
        /// </summary>
        /// <param name="id">The ID of the priority to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(Guid id)
        {
            var response = await _priorityService.DeletePriority(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}