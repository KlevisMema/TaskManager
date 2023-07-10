using Microsoft.AspNetCore.Mvc;
using TaskManager.DAL.DTO_s.Project;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var response = await _projectService.GetProjects();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Retrieves a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var response = await _projectService.GetProjectById(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="projectDto">The DTO containing the project data.</param>
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreateDto projectDto)
        {
            var response = await _projectService.CreateProject(projectDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Updates an existing project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to update.</param>
        /// <param name="projectDto">The DTO containing the updated project data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, ProjectUpdateDto projectDto)
        {
            var response = await _projectService.UpdateProject(id, projectDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Deletes a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var response = await _projectService.DeleteProject(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
