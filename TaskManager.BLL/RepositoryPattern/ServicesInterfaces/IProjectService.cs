using TaskManager.DAL.DTO_s.Project;
using TaskManager.HELPERS.ServiceResponse;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface IProjectService
    {
        Task<Response<List<ProjectDto>>> GetProjects();
        Task<Response<bool>> DeleteProject(Guid projectId);
        Task<Response<ProjectDto>> GetProjectById(Guid projectId);
        Task<Response<ProjectDto>> CreateProject(ProjectCreateDto projectDto);
        Task<Response<ProjectDto>> UpdateProject(Guid projectId, ProjectUpdateDto projectDto);
    }
}