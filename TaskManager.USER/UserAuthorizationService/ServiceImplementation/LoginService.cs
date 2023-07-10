#region Usings
using TaskManager.DAL.Models;
using TaskManager.DAL.Context;
using TaskManager.DTO.DTO_s.Login;
using Microsoft.AspNetCore.Identity;
using TaskManager.HELPERS.LogsHelper;
using TaskManager.HELPERS.ServiceResponse;
using TaskManagment.USER.UserAuthorizationService.ServiceInterfaces;
using TaskManagment.SECURITY.JWTAuthenticationService.ServiceInterface; 
#endregion

namespace TaskManagment.USER.UserAuthorizationService.ServiceImplementation
{
    public class LoginService : ILoginService
    {
        private readonly IOAuthJwtTokenService _oAuthService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public LoginService
        (
            UserManager<User> userManager,
            IOAuthJwtTokenService oAuthService,
            SignInManager<User> signInManager,
            ApplicationDbContext dbContext
        )
        {
            _userManager = userManager;
            _oAuthService = oAuthService;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<Response<string>>
        Login
        (
            DtoLogin logIn
        )
        {
            string UserId = "";

            try
            {
                var user = await _userManager.FindByEmailAsync(logIn.Email);

                if (user is null)
                    return Response<string>.NotFound("User not found");

                var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user!);

                if (!emailConfirmed)
                    return Response<string>.UnSuccessMessage("User email address not confirmed");

                var result = await _signInManager.PasswordSignInAsync(logIn.Email, logIn.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count == 0)
                        return Response<string>.NotFound("User has no roles");

                    var userTransformedObj = new TaskManager.DTO.DTO_s.User.UserDto()
                    {
                        Id = user!.Id,
                        Email = logIn.Email,
                        Roles = roles.ToList(),
                    };

                    UserId = user.Id;

                    return Response<string>.Ok($"{_oAuthService.CreateToken(userTransformedObj)}", "User logged in succsessfully");
                }

                return Response<string>.NotFound("User password invalid");
            }
            catch (Exception ex)
            {
                await LoggerHelper.LogException(ex, _dbContext);

                return Response<string>.ErrorMsg(ex.ToString());
            }
        }
    }
}