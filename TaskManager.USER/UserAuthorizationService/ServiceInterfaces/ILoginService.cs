using TaskManager.DAL.DTO_s.Login;
using TaskManager.HELPERS.ServiceResponse;

namespace TaskManagment.SECURITY.UserAuthorizationService.ServiceInterfaces
{
    public interface ILoginService
    {
        Task<Response<string>>Login(DtoLogin logIn);
    }
}