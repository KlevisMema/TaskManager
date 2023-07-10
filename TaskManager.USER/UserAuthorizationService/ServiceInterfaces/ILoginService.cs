#region Usings
using TaskManager.DAL.DTO_s.Login;
using TaskManager.HELPERS.ServiceResponse; 
#endregion

namespace TaskManagment.USER.UserAuthorizationService.ServiceInterfaces
{
    public interface ILoginService
    {
        Task<Response<string>>Login(DtoLogin logIn);
    }
}