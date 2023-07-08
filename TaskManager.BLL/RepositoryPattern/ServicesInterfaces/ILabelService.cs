using TaskManager.DAL.DTO_s.Label;
using TaskManager.BLL.ServiceResponse;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface ILabelService
    {
        Task<Response<LabelDto>> CreateLabel(LabelCreateDto labelDto);
        Task<Response<List<LabelDto>>> GetLabels();
        Task<Response<LabelDto>> GetLabelById(Guid labelId);
        Task<Response<LabelDto>> UpdateLabel(Guid labelId, LabelUpdateDto labelDto);
        Task<Response<bool>> DeleteLabel(Guid labelId);
    }
}