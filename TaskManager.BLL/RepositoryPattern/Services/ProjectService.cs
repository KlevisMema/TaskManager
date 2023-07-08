using AutoMapper;
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.BLL.BaseServices;
using TaskManager.DAL.DTO_s.Project;
using Microsoft.EntityFrameworkCore;
using TaskManager.BLL.ServiceHelpers;
using TaskManager.BLL.ServiceResponse;
using TaskManager.BLL.RepositoryPattern.ServicesInterfaces;

namespace TaskManager.BLL.RepositoryPattern.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService
        (
           IMapper mapper,
           ApplicationDbContext dbContext
        ) : base(mapper, dbContext)
        {

        }

        public async Task<Response<ProjectDto>> 
        CreateProject
        (
            ProjectCreateDto projectDto
        )
        {
            try
            {
                var project = _mapper.Map<Project>(projectDto);

                _dbContext.Projects.Add(project);
                await _dbContext.SaveChangesAsync();

                return Response<ProjectDto>.Ok(_mapper.Map<ProjectDto>(project), "Project created successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<ProjectDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<List<ProjectDto>>> 
        GetProjects()
        {
            try
            {
                var projects = await _dbContext.Projects.ToListAsync();

                return Response<List<ProjectDto>>.Ok(_mapper.Map<List<ProjectDto>>(projects), "Projects retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<List<ProjectDto>>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<ProjectDto>> 
        GetProjectById
        (
            Guid projectId
        )
        {
            try
            {
                var project = await _dbContext.Projects.FindAsync(projectId);
                if (project == null)
                    return Response<ProjectDto>.NotFound($"Project with id: {projectId} doesn't exist.");

                return Response<ProjectDto>.Ok(_mapper.Map<ProjectDto>(project), $"Project {project.Name} retrieved successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<ProjectDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<ProjectDto>> 
        UpdateProject
        (
            Guid projectId, 
            ProjectUpdateDto projectDto
        )
        {
            try
            {
                var project = await _dbContext.Projects.FindAsync(projectId);
                if (project == null)
                    return Response<ProjectDto>.NotFound($"Project with id: {projectId} doesn't exist.");

                _mapper.Map(projectDto, project);
                await _dbContext.SaveChangesAsync();

                return Response<ProjectDto>.Ok(_mapper.Map<ProjectDto>(project), $"Project with id: {projectId} updated successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<ProjectDto>.ErrorMsg(ex.ToString());
            }
        }

        public async Task<Response<bool>> 
        DeleteProject
        (
            Guid projectId
        )
        {
            try
            {
                var project = await _dbContext.Projects.FindAsync(projectId);
                if (project == null)
                    return Response<bool>.NotFound($"Project with id: {projectId} doesn't exist.");

                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();

                return Response<bool>.Ok(true, $"Project with id: {projectId} deleted successfully");
            }
            catch (Exception ex)
            {
                await ExceptionLogger.LogException(ex, _dbContext);

                return Response<bool>.ErrorMsg(ex.ToString());
            }
        }
    }
}