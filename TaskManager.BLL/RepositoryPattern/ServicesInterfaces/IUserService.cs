using TaskManager.DAL.DTO_s.User;
using TaskManager.BLL.ServiceResponse;

namespace TaskManager.BLL.RepositoryPattern.ServicesInterfaces
{
    public interface IUserService
    {
        Task<Response<List<UserDto>>> GetUsers();
        Task<Response<bool>> DeleteUser(string userId);
        Task<Response<UserDto>> GetUserById(string userId);
        Task<Response<UserDto>> CreateUser(UserCreateDto userDto);
        Task<Response<UserDto>> UpdateUser(string userId, UserUpdateDto userDto);
    }
}