using TaskManager.DAL.DTO_s.Project;
using TaskManager.BLL.ServiceResponse;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface IProjectService
    {
        Task<Response<ProjectDto>> CreateProject(ProjectCreateDto projectDto);
        Task<Response<List<ProjectDto>>> GetProjects();
        Task<Response<ProjectDto>> GetProjectById(Guid projectId);
        Task<Response<ProjectDto>> UpdateProject(Guid projectId, ProjectUpdateDto projectDto);
        Task<Response<bool>> DeleteProject(Guid projectId);
    }
}