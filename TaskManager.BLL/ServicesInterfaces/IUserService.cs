using TaskManager.DAL.DTO_s.User;
using TaskManager.BLL.ServiceResponse;

namespace TaskManager.BLL.ServicesInterfaces
{
    public interface IUserService
    {
        Task<Response<UserDto>> CreateUser(UserCreateDto userDto);
        Task<Response<UserDto>> GetUserById(string userId);
        Task<Response<UserDto>> UpdateUser(string userId, UserUpdateDto userDto);
        Task<Response<bool>> DeleteUser(string userId);
    }
}