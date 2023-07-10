using TaskManager.DAL.DTO_s.Priority;
using TaskManager.HELPERS.ServiceResponse;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface IPriorityService
    {
        Task<Response<List<PriorityDto>>> GetPriorities();
        Task<Response<bool>> DeletePriority(Guid priorityId);
        Task<Response<PriorityDto>> GetPriorityById(Guid priorityId);
        Task<Response<PriorityDto>> CreatePriority(PriorityCreateDto priorityDto);
        Task<Response<PriorityDto>> UpdatePriority(Guid priorityId, PriorityUpdateDto priorityDto);
    }
}