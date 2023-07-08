using TaskManager.DAL.DTO_s.User;

namespace TaskManagment.SECURITY.UserAuthenticationService.ServiceInterface
{
    public interface IOAuthJwtTokenService
    {
        string
       CreateToken(UserDto user);
    }
}